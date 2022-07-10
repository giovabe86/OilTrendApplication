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
        public static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=" + DB_FILE + "; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void StartupApplication(List<SourceBrentDataset> valuesBase)
        {
            try
            {
                if (!File.Exists(DB_FILE))
                {
                    SQLiteConnection.CreateFile(DB_FILE);
                }
                OilTrendVariationsCRUD.CreateTableTrendValues();
                OilTrendVariationsCRUD.DeleteAllOilTrendValues();
                OilTrendVariationsCRUD.AddOilTrendPrices(valuesBase);
            }
            catch
            {
                throw;
            }
        }    

    }
}
