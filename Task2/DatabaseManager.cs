using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HardwareScraper
{
    public class DatabaseManager
    {

        private static DatabaseManager instance = null;

        private readonly string connetionString = @"Data Source=ANDREA-PC\SQLEXPRESS01;Initial Catalog=WebScraperDatabase;User ID=sa;Password=";

        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }

                return instance;
            }
        }


        private DatabaseManager()
        {
        }

        public void TestConnection()
        {
            SqlConnection cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Database Connection Open  !");
            cnn.Close();
        }
    }
}
