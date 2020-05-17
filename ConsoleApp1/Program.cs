using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=ANDREA-PC\SQLEXPRESS01;Initial Catalog=WebScraperDatabase;User ID=sa;Password=";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            Console.WriteLine("Connection Open  !");


            SqlCommand command;
            SqlDataReader dataReader;
            string sql, output = "";
            sql = "select * from dbo.types";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while(dataReader.Read())
            {
                output = output + dataReader.GetValue(0)+ "\n";
            }

            Console.WriteLine(output);
            cnn.Close();

            Console.ReadLine();
        }
    }
}
