using System;
using System.Data.SqlClient;

namespace ChangeTownCasing
{
    public class Program
    {
        public static void Main()
        {
            string countryName = Console.ReadLine();

            SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated security=true");
            connection.Open();

            using (connection)
            {
                int countryId = GetCountryId(countryName, connection);

                UpdateTown(countryId, connection);
            }
        }

        private static int GetCountryId(string name, SqlConnection connection)
        {
            string selectQuery = "Select Id FROM Countries Where Name = @name";

            var command = GetSqlCommand(selectQuery, "@name", name, connection);
            int id = -1;

            var reader = command.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    id = (int) reader[0];
                }
            }

           return id;
        }

        private static void UpdateTown(int countryId, SqlConnection connection)
        {
            var updateQuery = "UPDATE Towns SET Name = UPPER(Name) Where CountryCode = @countryId";

            var command = GetSqlCommand(updateQuery, "@countryId", Convert.ToString(countryId), connection);

            int rowCount = command.ExecuteNonQuery();

            if (rowCount == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                var selectQuery = "Select [Name] from Towns where CountryCode = @countryId";
                var selectCommand = GetSqlCommand(selectQuery, "@countryId", Convert.ToString(countryId), connection);

                var reader = selectCommand.ExecuteReader();
                int index = 0;

                var townsNames = new string[rowCount];

                while (reader.Read())
                {
                    townsNames[index] = Convert.ToString(reader[0]);

                    index++;
                }

                reader.Close();

                Console.WriteLine($"{index} town names were affected.");

            Console.WriteLine("[" + string.Join(", ", townsNames) + "]");
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
