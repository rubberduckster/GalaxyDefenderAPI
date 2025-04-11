using Microsoft.AspNetCore.Mvc;
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
        //GetCharacter
        //GetCharacterById
        //CreateCharacter
        //UpdateCharacter
        //DeleteCharacter

        [HttpGet("get")]
        public string Farewell()
        {
            _characterService.GetCharacters();
            return "Farewell";
        }
    }
}
