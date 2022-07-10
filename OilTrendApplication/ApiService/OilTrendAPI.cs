using HttpJsonRpc;
using OilTrendApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OilTrendApplication.ApiService
{
    [JsonRpcClass("oilprice")]
    public class OilTrendAPI : BaseAPI
    {
        private IOilTrendService OilTrendService { get; }

        public OilTrendAPI(IOilTrendService OilTrendService)
        {
            this.OilTrendService = OilTrendService;
        }

        [JsonRpcMethod(Description = "Get Oil Trend by given dates")]
        public Task<Prices> TrendAsync(string startDateISO8601, string endDateISO8601)
        {
            var value = OilTrendService.Trend(startDateISO8601, endDateISO8601);
            return Task.FromResult(value);
        }

        [JsonRpcReceivedRequest]
        public async Task OnReceivedRequestAsync(JsonRpcContext context)
        {
            await Task.CompletedTask;

            Console.WriteLine("OnReceivedRequest in OilTrendApi");
        }

        [JsonRpcDeserializeParameter]
        public async Task<object> DeserializeParameterAsync(JsonElement value, ParameterInfo parameter, JsonSerializerOptions serializerOptions, JsonRpcContext context)
        {
            //This method can be used for custom parameter deserialization
            await Task.CompletedTask;

            return value.Deserialize(parameter.ParameterType, serializerOptions);
        }
    }
}
