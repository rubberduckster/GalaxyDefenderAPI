using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Repository;

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
    }
}