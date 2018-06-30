using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace IncreaseMinionsAge
{
    public class Program
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            using (connection)
            {
                UpdateMinionsAge(connection);
                PrintMinions(connection);
            }
        }

        private static void PrintMinions(SqlConnection connection)
        {
            var query = "Select Name, Age FROM Minions";

            var command = new SqlCommand(query, connection);

            var reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]}");
                }
            }
        }

        private static void UpdateMinionsAge(SqlConnection connection)
        {
            int[] ids = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var query = "Select Name FROM Minions Where Id = @Id";

            string updateQuery = "Update Minions SET Name = @name Where Id = @Id";

            for (int i = 0; i < ids.Length; i++)
            {
                var command = GetSqlCommand(query, "@id", Convert.ToString(ids[i]), connection);
                string name = Convert.ToString(command.ExecuteNonQuery());

                if (name != null)
                {
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    name = textInfo.ToTitleCase(name);

                    var updateCommand = GetSqlCommand(updateQuery, "@id", Convert.ToString(ids[i]), connection);

                    updateCommand.Parameters.AddWithValue("@name", name);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        private static SqlCommand GetSqlCommand(string query, string paramName, string paramValue, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(paramName, paramValue);

            return command;
        }
    }
}
