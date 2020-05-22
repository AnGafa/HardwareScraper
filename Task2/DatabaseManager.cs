using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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

        public Int32 InsertNewSearch(string searchKeyword)
        {
            String insertStatement = 
                @"INSERT INTO  SEARCHINFO (SearchKeyword, TimeStamp) 
                    OUTPUT INSERTED.SEARCHID
                    VALUES (@SearchKeyword, CURRENT_TIMESTAMP);";

            SqlConnection cnn = new SqlConnection(connetionString);

            Int32 newID;
            using (SqlCommand command = new SqlCommand(insertStatement, cnn))
            {
                command.Parameters.AddWithValue("@SearchKeyword", searchKeyword);

                cnn.Open();


                newID = (Int32)command.ExecuteScalar();

                command.Dispose();
                cnn.Close();
            }
            return newID;
        }

        internal void InsertNewSearchResults(int searchID, List<ResultItem> results)
        {
            String insertStatement =
                @"INSERT INTO  RESULTS (SearchID, SourceURL, ProductName) 
                    VALUES (@SearchID, @SourceURL, @ProductName);";


            SqlConnection cnn = new SqlConnection(connetionString);

            cnn.Open();


            foreach (ResultItem result in results)
            {

                using (SqlCommand command = new SqlCommand(insertStatement, cnn))
                {


                    command.Parameters.AddWithValue("@SearchID", searchID);
                    command.Parameters.AddWithValue("@SourceURL", result.SourceURL);
                    command.Parameters.AddWithValue("@ProductName", result.ProductName);

                    command.ExecuteNonQuery();

                    command.Dispose();

                }
            }
            cnn.Close();
        }

        public Int32 GetSearchInfoIDByKeyword(String searchKeyword)
        {

            String countStatement =
                @" select SearchId from SearchInfo
                    where SearchKeyword = @SearchKeyword;";

            SqlConnection cnn = new SqlConnection(connetionString);

            Int32 searchItemID = 0;
            try
            {
                using (SqlCommand command = new SqlCommand(countStatement, cnn))
                {
                    command.Parameters.AddWithValue("@SearchKeyword", searchKeyword);

                    cnn.Open();

                    searchItemID = (Int32)command.ExecuteScalar();
                    command.Dispose();


                    cnn.Close();
                }
            }
            catch(System.NullReferenceException)
            {
                cnn.Close();
            }

            return searchItemID;
        }


        public DateTime GetTimestampByID(Int32 searchID)
        {

            String selectStatement =
                @" select TimeStamp from SearchInfo
                    where SearchID = @SearchID;";

            SqlConnection cnn = new SqlConnection(connetionString);

            DateTime timeStamp;
            using (SqlCommand command = new SqlCommand(selectStatement, cnn))
            {
                command.Parameters.AddWithValue("@SearchID", searchID);

                cnn.Open();

                timeStamp = (DateTime)command.ExecuteScalar();
                command.Dispose();


                cnn.Close();
            }


            return timeStamp;
        }


        public void DeleteSearch(Int32 searchID)
        {

            String delteSearchInfo =
                @" delete from  SearchInfo
                    where SearchID = @SearchID;";

            String delteResults=
                @" delete from Results
                    where SearchID = @SearchID;";



            SqlConnection cnn = new SqlConnection(connetionString);

            using (SqlCommand command = new SqlCommand(delteResults, cnn))
            {
                command.Parameters.AddWithValue("@SearchID", searchID);

                cnn.Open();

                command.ExecuteNonQuery();
                command.Dispose();


                cnn.Close();
            }

            using (SqlCommand command = new SqlCommand(delteSearchInfo, cnn))
            {
                command.Parameters.AddWithValue("@SearchID", searchID);

                cnn.Open();

                command.ExecuteNonQuery();
                command.Dispose();


                cnn.Close();
            }
        }
    }
}
