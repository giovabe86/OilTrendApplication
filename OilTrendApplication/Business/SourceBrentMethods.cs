using OilTrendApplication.Model;
using System;
using System.Collections.Generic;
using System.Net;

namespace OilTrendApplication.Business
{
    public class SourceBrentMethods
    {
        /// <summary>
        /// Calls URL to retrieve Brent prices list
        /// </summary>
        /// <returns>List of brent prices list deserialized</returns>
        public static List<SourceBrentDataset> RetrieveSourceBrentDatasetAsync()
        {
            //Excute call and download json
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(Utils.Config.SourceBrentUrl);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SourceBrentDataset>>(json);
            }
        }
    }
}
