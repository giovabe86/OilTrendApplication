using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.Model
{
    public class SourceBrentDataset
    {
        [JsonProperty("Date")]
        public string Date { get; set; }
        [JsonProperty("Price")]
        public double Price { get; set; }

    }
}

