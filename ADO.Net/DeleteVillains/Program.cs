using System;
using System.Data.SqlClient;

namespace DeleteVillains
{
    public class Program
    {
        public static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection("Server=.;Database=MinionsDB;Integrated security=true");
            connection.Open();

            using (connection)
            {
               DeleteVillain(connection, villainId);
            }
        }

        private static void DeleteVillain(SqlConnection connection, int villainId)
        {
           var deleteFromMinionsVillains = "DELETE FROM MinionsVillains Where VillainId = @villainId";

            var getNameQuery = "Select Name From Villains Where Id = @villainId";

            var getNameCommand = GetSqlCommand(getNameQuery, "@villainId", Convert.ToString(villainId), connection);

            var villainName = (string)getNameCommand.ExecuteScalar();

            var deleteFromVillains = "Delete from Villains Where Id = @villainId";

            SqlCommand deleteMinionsVillainsCmd = GetSqlCommand(deleteFromMinionsVillains, "@villainId", Convert.ToString(villainId), connection);
            int realeasedMinions = (int)deleteMinionsVillainsCmd.ExecuteNonQuery();

            SqlCommand deleteVillainCmd = GetSqlCommand(deleteFromVillains, "@villainId", Convert.ToString(villainId), connection);

            int affectedRows = (int)deleteVillainCmd.ExecuteNonQuery();

            if (affectedRows != 0)
            {
                Console.WriteLine($"{villainName} was deleted");
                Console.WriteLine($"{realeasedMinions} minions released");
            }
            else
            {
                Console.WriteLine("No such villain was found");
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
