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

        public List<Character> GetCharacters(int? galaxyId)
        {
            List<Character> characters = new List<Character>();

            string sqlString = "SELECT * FROM Characters";

            if (galaxyId != null)
            {
                sqlString += $" WHERE GalaxyId = {galaxyId};";
            }

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string unitType = reader.GetString(3);
                int hp = reader.GetInt32(4);
                int def = reader.GetInt32(5);
                int dmg = reader.GetInt32(6);
                int range = reader.GetInt32(7);
                string talentName = reader.GetString(8);
                string talentDescription = reader.GetString(9);
                string femImg = reader.GetString(10);
                string mascImg = reader.GetString(11);
                int _galaxyId = reader.GetInt32(12);

                Character character = new Character(id, name, description, unitType, hp, def, dmg, range, talentName, talentDescription, femImg, mascImg, _galaxyId);
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
                result = new Character((int)reader["Id"], reader["Name"].ToString(), reader["Description"].ToString(), reader["UnitType"].ToString(), (int)reader["HP"], (int)reader["DEF"], (int)reader["DMG"], (int)reader["Range"], reader["TalentName"].ToString(), reader["TalentDescription"].ToString(), reader["FemIMG"].ToString(), reader["MascIMG"].ToString(), (int)reader["GalaxyId"]);
                connection.Close();
                return result;
            }

            connection.Close();

            return null;
        }

        public CharacterPaths[] GetPaths(int charId)
        {
            int index = 0;
            CharacterPaths[] paths = new CharacterPaths[9];

            string sqlstring = $"SELECT * FROM Paths WHERE CharacterId = {charId}";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlstring, connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int tier = reader.GetInt32(1);
                string pathName = reader.GetString(2);
                string name = reader.GetString(3);
                string description = reader.GetString(4);
                int hp = reader.GetInt32(5);
                int def = reader.GetInt32(6);
                int dmg = reader.GetInt32(7);
                int range = reader.GetInt32(8);
                string talentName = reader.GetString(9);
                string talentDescription = reader.GetString(10);
                string femImg = reader.GetString(11);
                string mascImg = reader.GetString(12);
                string characterImg = reader.GetString(13);
                int characterId = reader.GetInt32(14);

                CharacterPaths path = new CharacterPaths(id, tier, name, pathName, description, hp, def, dmg, range, talentName, talentDescription, femImg, mascImg, characterId);
                paths[index] = path;
                index++;
            }

            connection.Close();

            return paths;
        }

        public Character CreateCharacter(CharacterData data)
        {
            string sqlString = "INSERT INTO Characters ([Name], [Description], [UnitType], [HP], [DEF], [DMG], [Range], [TalentName], [TalentDescription], [FemIMG], [MascIMG], [GalaxyId]) " +
                $"OUTPUT INSERTED.Id VALUES ('{data.Name}', '{data.Description}', '{data.UnitType}', {data.HP}, {data.DEF}, {data.DMG}, {data.Range}, '{data.TalentName}', '{data.TalentDescription}', '{data.FemIMG}', '{data.MascIMG}', {data.GalaxyId});";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

            int id = (int)sqlCommand.ExecuteScalar();

            Character character = new Character(id, data.Name, data.Description, data.UnitType, data.HP, data.DEF, data.DMG, data.Range, data.TalentName, data.TalentDescription, data.FemIMG, data.MascIMG, data.GalaxyId);

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
            if (data.FemIMG == null)
            {
                data.FemIMG = character.FemIMG;
            }
            if (data.MascIMG == null)
            {
                data.MascIMG = character.MascIMG;
            }
            if (data.GalaxyId == 0)
            {
                data.GalaxyId = character.GalaxyId;
            }

            string sqlString = $"UPDATE Characters SET Name = '{data.Name}', Description = '{data.Description}', UnitType = '{data.UnitType}', HP = {data.HP}, DEF = {data.DEF}, DMG = {data.DMG}, Range = {data.Range}, TalentName = '{data.TalentName}', TalentDescription = '{data.TalentDescription}', FemIMG = '{data.FemIMG}', MascIMG = '{data.MascIMG}', GalaxyId = {data.GalaxyId} WHERE Id = {data.Id}";

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlString, connection);

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            return character;
        }
    }
}
