﻿using Microsoft.Data.Sqlite;
using System.Configuration;

namespace CodingTracker
{
    public static class CodingConfiguration
    {

        //private static string connectionString = @"Data Source=coding-Tracker.db";
        private static string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public static void CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS CodingSessions(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                StartTime TEXT,
                EndTime TEXT,
                Duration TEXT
                )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }


        }

        public static void GetUserInput()
        {
            Console.Clear();

            bool closeApp = false;
            while (closeApp == false)
            {

                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("\nType 1 to View All Records.");
                Console.WriteLine("\nType 2 to Insert Record.");
                Console.WriteLine("\nType 3 to Delete Record.");
                Console.WriteLine("\nType 4 to Update Record.");
                Console.WriteLine("\n---------------------------------------\n");

                string? commandInput = Console.ReadLine();

                switch (commandInput)
                {
                    case "0":
                        Console.WriteLine("\nGoodbye\n");
                        closeApp = true;
                        break;

                    //case "1":
                    //    GetAllRecords();
                    //    break;

                    case "2":
                        Insert();
                        break;

                    //case "3":
                    //    Delete();
                    //    break;

                    //case "4":
                    //    Update();
                    //    break;

                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;

                }


            }
        }

        private static void Insert()
        {

            Console.WriteLine("Insert function");

            TimeOnly startDate = GetTimeInput("Please insert the start time (format HH:MM - e.g. 12:30). Press 0 to return to menu.");
            TimeOnly endDate = GetTimeInput("\"Please insert the end time (format HH:MM - e.g. 12:30). Press 0 to return to menu.");



            //string? habit = GetHabitInput();

            //int quantity = GetNumberInput("\n\nPlease insert number of times habit was completed (no decimals allowed)\n\n");

            //using (var connection = new SqliteConnection(connectionString))
            //{
            //    connection.Open();
            //    var tableCmd = connection.CreateCommand();

            //    tableCmd.CommandText =
            //               $"INSERT INTO habits(date, quantity) VALUES('{habit}', {quantity})";

            //    Console.WriteLine($"Executing command: {tableCmd.CommandText.ToString()}");

            //    tableCmd.ExecuteNonQuery();

            //    connection.Close();
            //}
        }

        private static TimeOnly GetTimeInput(string message)
        {

            Console.WriteLine(message);
            string? input = Console.ReadLine();

            if (input == "0") GetUserInput();

            TimeOnly time;


            return TimeOnly.FromDateTime(DateTime.Now);
        }

    }
}