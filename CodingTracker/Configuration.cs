using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;


namespace CodingTracker
{
    internal class Configuration
    {

        private static string connectionString = @"Data Source=coding-Tracker.db";

        static void CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS coding(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                

";






            }


        }

    }
}
