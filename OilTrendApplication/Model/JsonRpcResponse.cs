using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Price
    {
        public string dateISO8601 { get; set; }
        public double price { get; set; }
    }

    public class Result
    {
        public List<Price> prices { get; set; }
    }

    public class JsonRpcResponse
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        public Result result { get; set; }
        public Error error { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string message { get; set; }
        public string stackTrace { get; set; }
    }
}
