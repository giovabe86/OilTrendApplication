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
            String completeUrl = System.Configuration.ConfigurationManager.AppSettings["SourceBrentUrl"];

            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(completeUrl);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<SourceBrentDataset>>(json);
            }
        }
    }
}
