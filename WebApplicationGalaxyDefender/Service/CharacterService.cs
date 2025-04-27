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

        public List<Character> GetCharacters()
        {
            return _characterrepos.GetCharacters();
        }

        public Character PostCharacter(CharacterData data)
        {
            return _characterrepos.CreateCharacter(data);
        }

        public void DeleteCharacter(int characterId) 
        {
            _characterrepos.DeleteCharacter(characterId);
        }
    }
}