using OilTrendApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.ApiService
{
    public class OilTrendService : IOilTrendService
    {
        public Prices Trend(string startDateISO8601, string endDateISO8601)
        {
             return Persistency.OilTrendVariationsCRUD.GetOilTrendValues(startDateISO8601, endDateISO8601);
        }
    }
}
