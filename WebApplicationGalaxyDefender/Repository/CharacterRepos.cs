using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using WebApplicationGalaxyDefender.DataModels;
using WebApplicationGalaxyDefender.Model;

namespace WebApplicationGalaxyDefender.Repository
{
    public interface ICharacterRepos
    {

    }

    public class CharacterRepos
    {
        public SqlConnection connection = new SqlConnection("Server=localhost;Database=GalaxyDefenders;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");


        public List<Character> GetCharacters(int? galaxyIdd)
        {
            List<Character> characters = new List<Character>();

            string sqlString = $"SELECT * FROM Characters WHERE GalaxyId = {galaxyIdd};";

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

        public Character GetCharacterById(int characterId) 
        {
            string sqlString = $"SELECT * FROM Characters WHERE Id = {characterId}";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            Character result;

            if (reader.Read())
            {
                result = new Character((int)reader["Id"], reader["Gender"].ToString(), reader["Name"].ToString(), reader["Description"].ToString(), reader["UnitType"].ToString(), (int)reader["HP"], (int)reader["DEF"], (int)reader["DMG"], (int)reader["Range"], reader["TalentName"].ToString(), reader["TalentDescription"].ToString(), reader["CharacterIMG"].ToString(), (int)reader["GalaxyId"]);
                connection.Close();
                return result;
            }

            connection.Close();
            return null;
        }

        
        public Character CreateCharacter(CharacterData data)
        {

            string sqlString = "INSERT INTO Characters ([Gender], [Name], [Description], [UnitType], [HP], [DEF], [DMG], [Range], [TalentName], [TalentDescription], [CharacterIMG], [GalaxyId]) " +
                $"OUTPUT INSERTED.Id VALUES ('{data.Gender}', '{data.Name}', '{data.Description}', '{data.UnitType}', {data.HP}, {data.DEF}, {data.DMG}, {data.Range}, '{data.TalentName}', '{data.TalentDescription}', '{data.CharacterIMG}', {data.GalaxyId});";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

            int id = (int)sqlCommand.ExecuteScalar();

            Character character = new Character(id, data.Gender, data.Name, data.Description, data.UnitType, data.HP, data.DEF, data.DMG, data.Range, data.TalentName, data.TalentDescription, data.CharacterIMG, data.GalaxyId);


            connection.Close();

            return character;
        }

        public void DeleteCharacter(int characterId)
        {
            string sqlString = $"DELETE FROM Characters WHERE id = '{characterId}';";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }

        public Character UpdateCharacter(CharacterData data)
        {
            GetCharacterById(data.Id);

            Character character = GetCharacterById(data.Id);

            if (data.Gender == null)
            {
                data.Gender = character.Gender;
            }
            if (data.Name == null)
            {
                data.Name = character.Name;
            }
            if (data.Description == null)
            {
                data.Description = character.Description;
            }
            if (data.UnitType == null)
            {
                data.UnitType = character.UnitType;
            }
            if (data.HP == 0)
            {
                data.HP = character.HP;
            }
            if (data.DEF == 0)
            {
                data.DEF = character.DEF;
            }
            if (data.TalentName == null)
            {
                data.TalentName = character.TalentName;
            }
            if (data.TalentDescription == null)
            {
                data.TalentDescription = character.TalentDescription;
            }
            if (data.CharacterIMG == null)
            {
                data.CharacterIMG = character.CharacterIMG;
            }
            if (data.GalaxyId == 0)
            {
                data.GalaxyId = character.GalaxyId;
            }

            string sqlString = $"UPDATE Characters SET Gender = '{data.Gender}', Name = '{data.Name}', Description = '{data.Description}', UnitType = '{data.UnitType}', HP = {data.HP}, DEF = {data.DEF}, DMG = {data.DMG}, Range = {data.Range}, TalentName = '{data.TalentName}', TalentDescription = '{data.TalentDescription}', CharacterIMG = '{data.CharacterIMG}', GalaxyId = {data.GalaxyId} WHERE Id = {data.Id}";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            return character;
        }
    }
}
