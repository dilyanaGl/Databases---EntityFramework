using System;
using System.Data.SqlClient;

namespace WriteVillainNames
{
    public class Program
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            using (connection)
            {
                var query =
                    "SELECT v.[Name], COUNT(mv.MinionId) FROM Villains as v JOIN MinionsVillains as mv ON mv.VillainId = v.Id GROUP BY v.[Name]  HAVING COUNT(mv.MinionId)>= 3 ORDER BY COUNT(mv.MinionId) DESC";

                var command = new SqlCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }
            }
        }
    }
}
