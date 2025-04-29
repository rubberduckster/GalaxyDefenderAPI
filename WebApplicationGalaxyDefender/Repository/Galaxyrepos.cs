using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using WebApplicationGalaxyDefender.DataModels;
using WebApplicationGalaxyDefender.Model;

namespace WebApplicationGalaxyDefender.Repository
{

    public interface IGalaxyRepos
    {

    }

    public class Galaxyrepos
    {
        public SqlConnection connection = new SqlConnection("Server=localhost;Database=GalaxyDefenders;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

        public List<Galaxy> GetGalaxies()
        {
            List<Galaxy> galaxies = new List<Galaxy>();

            string sqlString = "SELECT * FROM Galaxies;";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                Galaxy galaxy = new Galaxy(id, name);
                galaxies.Add(galaxy);
            }

            connection.Close();

            return galaxies;
        }

        public Galaxy GetGalaxyById(int galaxyId)
        {
            string sqlString = $"SELECT * FROM Galaxies WHERE Id = {galaxyId}";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            Galaxy result;

            if (reader.Read())
            {
                result = new Galaxy((int)reader["Id"], reader["Name"].ToString());
                connection.Close();
                return result;
            }

            connection.Close();
            return null;
        }
    }
}
