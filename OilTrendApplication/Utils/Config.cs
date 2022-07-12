using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.Utils
{
    /// <summary>
    /// Retriving data from app.config
    /// </summary>
    public static class Config
    {
        public static String ServerUrl
        {
            get => System.Configuration.ConfigurationManager.AppSettings["ServerUrl"];
        }

        public static Int32 ServerUrlPort
        {
            get => Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ServerUrlPort"]);
        }

        public static String SourceBrentUrl
        {
            get => System.Configuration.ConfigurationManager.AppSettings["SourceBrentUrl"];
        }
    }
}
