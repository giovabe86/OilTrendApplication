using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using HttpJsonRpc;
using Microsoft.Extensions.Logging;
using OilTrendApplication.ApiService;
using OilTrendApplication.Model;
using OilTrendApplication.Persistency;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication
{

    [JsonRpcClass("program")]
    class Program
    {
        static async Task Main(string[] args)
        {
            //Configure JsonRpc to use Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            JsonRpc.LoggerFactory = new LoggerFactory().AddSerilog();
            //Custom error handling
            JsonRpc.OnError(e =>
            {
                Debug.WriteLine(e.ToString());
            });
            //Retrieving data from Json on site
            var sourceBrentDataset = Business.SourceBrentMethods.RetrieveSourceBrentDatasetAsync();
            //Loading SQLite configuration and insert 
            SQLiteManager.LoadSQLiteOnStartupApplication(sourceBrentDataset);
            //Open connection on server host 
            JsonRpc.ServerOptions = (o) =>
            {
                o.Listen(new IPEndPoint(IPAddress.Parse(Utils.Config.ServerUrl), Utils.Config.ServerUrlPort));
            };
            //Setup optional dependency injection
            var builder = new ContainerBuilder();
            builder.RegisterType<OilTrendAPI>();
            builder.RegisterType<OilTrendService>().As<IOilTrendService>();
            var container = builder.Build();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
            try
            {
                JsonRpc.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
            await JsonRpc.StopAsync();
        }

        [JsonRpcMethod]
        public static Task WriteLineAsync(string message)
        {
            Console.WriteLine(message);

            return Task.CompletedTask;
        }

        [JsonRpcMethod]
        public static Task ThrowErrorAsync(string message)
        {
            throw new Exception(message);
        }
    }
}
