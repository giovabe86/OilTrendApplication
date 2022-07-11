using OilTrendApplication.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilTrendApplication.Persistency
{
    public class SQLiteManager
    {
        private static String DB_FILE= "OilTrendDB.sqlite";
        private static SQLiteConnection sqliteConnection;
        public SQLiteManager()
        { }
        /// <summary>
        /// Db object for Sqlite connection
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + DB_FILE + "; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        /// <summary>
        /// Method called during startup application to create and insert values in Sqlite db
        /// </summary>
        /// <param name="valuesBase">List of values retreived calling the oil brent list</param>
        public static void StartupApplication(List<SourceBrentDataset> valuesBase)
        {
            try
            {
                if (!File.Exists(DB_FILE))
                {
                    SQLiteConnection.CreateFile(DB_FILE);
                }
                //Create table if not exists
                OilTrendVariationsCRUD.CreateTableTrendValues();
                //Clean table if exists
                OilTrendVariationsCRUD.DeleteAllOilTrendValues();
                //Insert values in OilTrendPrices
                OilTrendVariationsCRUD.AddOilTrendPrices(valuesBase);
            }
            catch
            {
                throw;
            }
        }    

    }
}
