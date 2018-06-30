using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GetMinionNames
{
    public class Program
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            List<string> names = new List<string>();

            using (connection)
            {
               names = GetMinionNames(connection);
            }

            int half = names.Count / 2;

            for (int i = 0; i < half; i++)
            {
                Console.WriteLine(names[i]);
                Console.WriteLine(names[names.Count - 1 - i]);
            }

            if (names.Count % 2 == 1)
            {
                Console.WriteLine(names[half]);
            }
        }

        private static List<string> GetMinionNames(SqlConnection connection)
        {
            var selectQuey = "Select Name from Minions";

            var command = new SqlCommand(selectQuey, connection);

            var reader = command.ExecuteReader();

            var minions = new List<string>();

            while (reader.Read())
            {
                minions.Add(Convert.ToString(reader[0]));
            }

            reader.Close();

            return minions;
        }
    }
}
