using System;
using System.Data.SqlClient;
using System.IO;

namespace InitialiseDatabase
{
    public class Program
    {
        public static void Main()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                ["Server"] = ".",
                ["Integrated security"] = "true"
            };

            var connection = new SqlConnection(builder.ToString());

            connection.Open();

            using (connection)
            {
                SetDatabase(connection);

                CreateDbTables(connection);
            }
        }

        private static void CreateDbTables(SqlConnection connection)
        {
            var query = File.ReadAllText("MinionsDB.sql");

            SqlCommand command = new SqlCommand(query, connection);
            Console.WriteLine("Created DB tables. Rows affected {0}.", command.ExecuteNonQuery());
        }

        private static void SetDatabase(SqlConnection connection)
        {
            try
            {
                var createQuery = "CREATE DATABASE MinionsDB";

                var createCommand = new SqlCommand(createQuery, connection);
                createCommand.ExecuteNonQuery();

                var useQuery = "USE MinionsDB";

                var useCommand = new SqlCommand(useQuery, connection);
                useCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
