using System;
using System.Data.SqlClient;

namespace IncreaseAgeWithProcedure
{
    public class Program
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            int minionId = int.Parse(Console.ReadLine());

            using (connection)
            {
                UpdateMinionsAge(connection, minionId);
                PrintMinions(connection, minionId);
            }
        }

        private static void PrintMinions(SqlConnection connection, int minionId)
        {
            var query = "Select Name, Age FROM Minions Where Id = @minionId";

            var command = GetSqlCommand(query, "@minionId", Convert.ToString(minionId), connection);

            var reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]} years old");
                }
            }
        }

        private static void UpdateMinionsAge(SqlConnection connection, int minionId)
        {
            var execProcedure = @"EXEC usp_GetOlder @id";

            SqlCommand command = GetSqlCommand(execProcedure, "@id", Convert.ToString(minionId), connection);
            command.ExecuteNonQuery();
        }

        private static SqlCommand GetSqlCommand(string query, string paramName, string paramValue, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(paramName, paramValue);

            return command;
        }
    }
}
