using System;
using System.Data.SqlClient;

namespace GetMinionsNames
{
    public class Program
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            int villainId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string villainQuery = "Select [Name] from Villains Where id = @villainId";

                var villainCommand = new SqlCommand(villainQuery, connection);
                villainCommand.Parameters.AddWithValue("@villainId", villainId);

                var reader = villainCommand.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Villain: {reader[0]}");
                }

                reader.Close();

                string minonsCountQuery = "Select m.Name, m.Age FROM Minions as m JOIN MinionsVillains as mv ON m.Id = mv.MinionId Where mv.VillainId = @villainId ORDER BY m.Name";

                var minonsCountCommand = new SqlCommand(minonsCountQuery, connection);

                minonsCountCommand.Parameters.AddWithValue("@villainId", villainId);

                reader = minonsCountCommand.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }

                reader.Close();
            }

            connection.Close();
        }
    }
}
