using OilTrendApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.ApiService
{
    public interface IOilTrendService
    {
       Prices Trend(string startDateISO8601, string endDateISO8601);
    }
}
