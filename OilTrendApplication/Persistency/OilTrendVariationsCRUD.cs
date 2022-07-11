using OilTrendApplication.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.Persistency
{
    public static class OilTrendVariationsCRUD
    {

        /// <summary>
        /// Table OilTrendPrices creation query
        /// </summary>
        public static void CreateTableTrendValues()
        {
            try
            {
                using (var cmd = SQLiteManager.DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS OilTrendPrices (dateISO8601 TEXT, price DECIMAL(10,2))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves all oil trend values by given date interval.
        /// </summary>
        /// <param name="StartDate">
        /// Interval start date. If empty, searching startless limit
        /// </param>
        /// <param name="EndDate">Interval end date if empty searching endless</param>
        /// <returns></returns>
        public static Prices GetOilTrendValues(String StartDate, String EndDate)
        {
            Prices prices = new Prices();
            List<OilTrendValuesReponse> datasets = new List<OilTrendValuesReponse>();
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                //Check date values isoCode
                if (!String.IsNullOrEmpty(StartDate)) { DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture); }
                if (!String.IsNullOrEmpty(EndDate)){DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);}
                using (var cmd = SQLiteManager.DbConnection().CreateCommand())
                {
                    //Query creation
                    StringBuilder query = new StringBuilder("SELECT * FROM OilTrendPrices ");

                    if (String.IsNullOrEmpty(StartDate)) { StartDate = "1900-01-01"; }
                    if (String.IsNullOrEmpty(EndDate)) { EndDate = "now"; }

                    query.Append($" WHERE date(dateISO8601) BETWEEN date('{StartDate}') AND date('{EndDate}')");

                    cmd.CommandText = query.ToString();
                    da = new SQLiteDataAdapter(cmd.CommandText, SQLiteManager.DbConnection());
                    da.Fill(dt);
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        OilTrendValuesReponse source = new OilTrendValuesReponse();
                        source.dateISO8601 = (string)dr[0];
                        source.price = Convert.ToDecimal(dr[1]);
                        datasets.Add(source);
                    }
                    prices.prices = datasets;
                }
                return prices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Bulk insert of oil trend values in OilTrendPrices
        /// </summary>
        /// <param name="valueBase">List of values</param>
        public static void AddOilTrendPrices(List<SourceBrentDataset> valueBase)
        {
            try
            {
                using (var conn = SQLiteManager.DbConnection())
                {
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            foreach (var current in valueBase)
                            {
                                cmd.CommandText = "INSERT INTO OilTrendPrices(dateISO8601, price) VALUES (@date, @price)";
                                cmd.Parameters.AddWithValue("@date", current.Date);
                                cmd.Parameters.AddWithValue("@price", current.Price);
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Cleans all values form OilTrendPrices table
        /// </summary>
        public static void DeleteAllOilTrendValues()
        {
            try
            {
                using (var cmd = new SQLiteCommand(SQLiteManager.DbConnection()))
                {
                    
                    cmd.CommandText = "DELETE FROM OilTrendPrices";
                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}