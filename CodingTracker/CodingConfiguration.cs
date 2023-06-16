using Microsoft.Data.Sqlite;
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

                    case "1":
                        GetAllRecords();
                        break;

                    case "2":
                        Insert();
                        break;

                    case "3":
                        Delete();
                        break;

                    case "4":
                        Update();
                        break;

                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;

                }


            }
        }

        static void Delete()
        {
            Console.Clear();
            GetAllRecords();

            var recordId = GetNumberInput("\n\nPlease type the Id of the record you want to delete or type 0 to go back to Main Menu\n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from CodingSessions WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} not found");
                    Delete();
                }

            }

            Console.WriteLine($"\n\nRecord with Id {recordId} was deleted. \n\n");

            GetUserInput();
        }

        static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string? numberInput = Console.ReadLine();

            if (numberInput == "0") GetUserInput();

            if (!String.IsNullOrEmpty(numberInput))
            {
                return Convert.ToInt32(numberInput);
            }

            else return -1;
        }

        private static void Insert()
        {

            Console.WriteLine("Insert function");

            TimeOnly startDate = GetTimeInput("Please insert the start time (format HH:MM - e.g. 12:30). Press 0 to return to menu.");
            TimeOnly endDate = GetTimeInput("\"Please insert the end time (format HH:MM - e.g. 12:30). Press 0 to return to menu.");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                           $"INSERT INTO CodingSessions(StartTime, EndTime, Duration) VALUES('{startDate.ToString()}', '{endDate.ToString()}', '{GetTimeDifference(startDate, endDate)}')";

                Console.WriteLine($"Executing command: {tableCmd.CommandText.ToString()}");

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        static void Update()
        {
            Console.Clear();
            GetAllRecords();

            var recordId = GetNumberInput("\n\nPlease type Id of the habit you would like to update. Type 0 to return to main manu.\n\n");

            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();
                var checkCmd = connection.CreateCommand();

                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM CodingSessions WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                    connection.Close();
                    Update();
                }

                TimeOnly startDate = GetTimeInput("Please insert the start time (format HH:MM - e.g. 12:30). Press 0 to return to menu.");
                TimeOnly endDate = GetTimeInput("\"Please insert the end time (format HH:MM - e.g. 12:30). Press 0 to return to menu.");

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"UPDATE CodingSessions SET StartTime = '{startDate.ToString()}', EndTime = '{endDate.ToString()}' WHERE Id = '{recordId}'";

                tableCmd.ExecuteNonQuery();

                connection.Close();

            }

        }

        private static void GetAllRecords()
        {
            Console.Clear();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM CodingSessions";

                List<CodingSession> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            StartDate = reader.GetString(1),
                            EndDate = reader.GetString(2)
                        }); ;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found");
                }

                connection.Close();

                Console.WriteLine("------------------------------------------\n");
                foreach (var dw in tableData)
                {
                    Console.WriteLine($"{dw.Id} - Start Date: {dw.StartDate} - End date: {dw.EndDate}. Duration: {GetTimeDifference(TimeOnly.Parse(dw.StartDate), TimeOnly.Parse(dw.EndDate))}");

                }
                Console.WriteLine("------------------------------------------\n");
            }
        }

        private static TimeOnly GetTimeInput(string message)
        {

            Console.WriteLine(message);
            string? input = Console.ReadLine();

            if (input == "0") GetUserInput();

            TimeOnly time;

            if (!String.IsNullOrEmpty(input) && input.Length > 4)
            {
                time = TimeOnly.Parse(input);
            }


            return time;
        }

        private static string GetTimeDifference(TimeOnly start, TimeOnly end)
        {
            return (end - start).ToString();
        }

    }
}
