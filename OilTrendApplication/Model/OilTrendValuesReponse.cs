using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.Model
{

    public class Prices
    {
        [JsonProperty("prices")]
        public List<OilTrendValuesReponse> prices { get; set; }
    }

    public class OilTrendValuesReponse
    {

        [JsonProperty("dateISO8601")]
        public string dateISO8601 { get; set; }
        [JsonProperty("price")]
        public decimal price { get; set; }
    }
}
