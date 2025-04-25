using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.Language.Extensions;
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

            string genderInput = "M";
            string nameInput = "M";
            string descriptionInput = "M";
            string unitTypeInput = "M";
            int hpInput = 1;
            int defInput = 1;                
            int dmgInput = 1;
            int rangeInput = 1;
            string talentNameInput = "M";
            string talentDescriptionInput = "M";
            string characterImgInput = "M";
            int galaxyIdInput = 1;

            string sqlString = "INSERT INTO Characters ([Gender], [Name], [Description], [UnitType], [HP], [DEF], [DMG], [Range], [TalentName], [TalentDescription], [CharacterIMG], [GalaxyId]) " +
                $"VALUES ('{genderInput}', '{nameInput}', '{descriptionInput}', '{unitTypeInput}', {hpInput}, {defInput}, {dmgInput}, {rangeInput}, '{talentNameInput}', '{talentDescriptionInput}', '{characterImgInput}', {galaxyIdInput});\r\nGO";

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
            }

            //sqlCommand.ExecuteNonQuery();

            connection.Close();

            //gets latest added characters ID
            string sqlStringId = "SELECT SCOPE_IDENTITY() AS [Character_Id];\r\nGO";

            return sqlStringId;


            //how to send a body with it
        }

        public int DeleteCharacter(Character charcter)
        {
            int characteId = 7;

            string sqlString = $"DELETE FROM Characters WHERE id = '{}';";

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            sqlCommand.ExecuteNonQuery();
        }
    }
}
