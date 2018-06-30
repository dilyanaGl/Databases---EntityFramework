using System;
using System.Data.SqlClient;
using System.Linq;

namespace AddMinion
{
    public class Program
    {
        public static void Main()
        {
            var minionString = Console.ReadLine();
            var villainString = Console.ReadLine();

            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            using (connection)
            {
                var villainName = villainString.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                var minionPart = minionString.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                var minonArgs = minionPart[1].Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string minionName = minionPart[1];
                int age = int.Parse(minionPart[2]);
                string town = minionPart[3];

                int townId = AddTownReturnId(connection, town);

                int villainId = AddVillain(connection, villainName);

                int minionId = AddMinion(connection, minionName, age, townId);

                AddMinionToVillain(connection, minionId, minionName, villainId, villainName);
            }
        }

        private static int AddTownReturnId(SqlConnection connection, string name)
        {
            string sqlQuery = "Select Id From Towns Where Name = @Name";

            SqlCommand searchTown = GetSqlCommand(sqlQuery, "@name", name, connection);

            var addTownQuery = "Insert into Towns(Name, CountryCode) VALUES (@name, NULL)";
            SqlCommand addTown = GetSqlCommand(addTownQuery, "@name", name, connection);


            string message = $"Town {name} was added to the database";

            return CheckOrAddMethod(searchTown, addTown, message);

        }

        private static int AddVillain(SqlConnection connection, string name)
        {
            string sqlQuery = "Select Id from Villains Where Name = @name";

            var command = GetSqlCommand(sqlQuery, "@name", name, connection);

            string insertQuery = "INSERT INTO Villains(Name, EvilnessFactorId) VALUES (@name, 4)";
            var insertCommand = GetSqlCommand(insertQuery, "@name", name, connection);

           string message = $"Villain {name} was added to the database.";

            return CheckOrAddMethod(command, insertCommand, message);
        }

        private static void AddMinionToVillain(SqlConnection connection, int minionId, string minionName, int villainId,
            string villainName)
        {
            var selectQuery = "SELECT COUNT(*) AS Count FROM MinionsVillains WHERE MinionId = @minionId AND VillainId = @villainId";

            var insertQuery = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES(@minionId, @villainId)";

            SqlCommand searchMinionVillainCmd = GetSqlCommand(selectQuery, "@minionId", minionId.ToString(), connection);
            searchMinionVillainCmd.Parameters.AddWithValue("@villainId", villainId);
            int count = (int?)searchMinionVillainCmd.ExecuteScalar() ?? 0;

            while (count == 0)
            {
                SqlCommand addMinionVillainCmd = GetSqlCommand(insertQuery, "@minionId", minionId.ToString(), connection);
                addMinionVillainCmd.Parameters.AddWithValue("@villainId", villainId.ToString());
                int affectedRows = (int)addMinionVillainCmd.ExecuteNonQuery();
                if (affectedRows != 0)
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");

                count = (int?)searchMinionVillainCmd.ExecuteScalar() ?? 0;
            }
        }

        private static int AddMinion(SqlConnection connection, string name, int age, int townId)
        {
            string selectQuery = "Select id FROM Minions Where Name = @Name";

            var selectCommand = GetSqlCommand(selectQuery, "@name", name, connection);

            string insertQuery = "INSERT INTO Minions(name, age, townId) VALUES (@name, @age, @townId)";

            var insertCommand = GetSqlCommand(insertQuery, "@name", name, connection);

            insertCommand.Parameters.AddWithValue("@age", age);

            insertCommand.Parameters.AddWithValue("@townId", townId);

            return CheckOrAddMethod(selectCommand, insertCommand, null);
        }

        private static int CheckOrAddMethod(SqlCommand select, SqlCommand insert, string message)
        {
            int id = (int?) select.ExecuteScalar() ?? -1;

            while (id == -1)
            {
                int affectedRows = (int)insert.ExecuteNonQuery();

                    if (affectedRows != 0)
                    {
                        Console.WriteLine(message);
                    }

                    id = (int?) select.ExecuteScalar() ?? -1;
                }

            return id;
        }

        private static SqlCommand GetSqlCommand(string query, string paramName, string paramValue, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(paramName, paramValue);

            return command;
        }
    }
}

