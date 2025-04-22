using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplicationGalaxyDefender.Model;

namespace WebApplicationGalaxyDefender.Repository
{
    public interface ICharacterRepos
    {

    }

    public class CharacterRepos
    {
        public SqlConnection connection = new SqlConnection("Server=localhost;Database=GalaxyDefenders;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");


        public List<Character> GetCharacters()
        {
            List<Character> characters = new List<Character>();

            string sqlString = "SELECT * FROM Characters;";

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string gender = reader.GetString(1);
                string name = reader.GetString(2);
                string description = reader.GetString(3);
                string unitType = reader.GetString(4);
                int hp = reader.GetInt32(5);
                int def = reader.GetInt32(6);
                int dmg = reader.GetInt32(7);
                int range = reader.GetInt32(8);
                string talentName = reader.GetString(9);
                string talentDescription = reader.GetString(10);
                string characterImg = reader.GetString(11);
                int galaxyId = reader.GetInt32(12);

                Character character = new Character(id, gender, name, description, unitType, hp, def, dmg, range, talentName, talentDescription, characterImg, galaxyId);
                characters.Add(character);
            }
            connection.Close();

            return characters;
        }

        public int CreateCharacter(Character charcter)
        {
            connection.Open();

            string sqlString = "";

            //return character Id
        }
    }
}
