using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplicationGalaxyDefender.Repository
{
    public interface ICharacterRepos
    {

    }

    public class CharacterRepos
    {
        public SqlConnection connection;

        public void GetCharacters()
        {
            connection = new SqlConnection("Server=localhost;Database=GalaxyDefenders;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

            string sqlString = "SELECT * FROM Characters FOR JSON AUTO;";

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine(reader[i]);
                }
            }
            connection.Close();
        }
    }
}
