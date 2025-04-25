using Microsoft.AspNetCore.Mvc;
using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Service;

namespace WebApplicationGalaxyDefender.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharacterController : ControllerBase
    {

        private CharacterService _characterService;

        public CharacterController(CharacterService characterService) {
            _characterService = characterService;
        }
        //GetCharacterById
        //UpdateCharacter
        //DeleteCharacter

        [HttpGet]
        //Get all characters
        public List<Character> CharactersGet()
        {
            return _characterService.GetCharacters();
        }

        [HttpPost]
        //Make a Character
        public string CharactersPost()
        {
            return _characterService.PostCharacter();
        }
    }
}
