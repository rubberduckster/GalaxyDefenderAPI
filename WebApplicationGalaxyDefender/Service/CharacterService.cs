using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Repository;
using WebApplicationGalaxyDefender.DataModels;

namespace WebApplicationGalaxyDefender.Service
{
    public class CharacterService
    {
        private CharacterRepos _characterrepos;

        public CharacterService(CharacterRepos characterrepos)
        {
            _characterrepos = characterrepos;
        }

        public List<Character> GetCharacters(int? galaxyId)
        {
            return _characterrepos.GetCharacters(galaxyId);
        }

        public Character GetCharacterById(int characterId)
        {
            return _characterrepos.GetCharacterById(characterId);
        }

        public CharacterPaths[] GetCharacterPaths(int charId)
        {
            return _characterrepos.GetPaths(charId);
        }

        public Character PostCharacter(CharacterData data)
        {
            return _characterrepos.CreateCharacter(data);
        }

        public void DeleteCharacter(int characterId) 
        {
            _characterrepos.DeleteCharacter(characterId);
        }

        public Character PutCharacter(CharacterData data)
        {
            return _characterrepos.UpdateCharacter(data);
        }
    }
}