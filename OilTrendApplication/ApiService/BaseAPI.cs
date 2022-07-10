using HttpJsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.ApiService
{
    public abstract class BaseAPI
    {
        [JsonRpcMethod]
        public Task<string> GetTypeNameAsync()
        {
            return Task.FromResult(GetType().Name);
        }
    }
}
