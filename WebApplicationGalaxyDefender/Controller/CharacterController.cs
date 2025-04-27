using Microsoft.AspNetCore.Mvc;
using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Service;
using WebApplicationGalaxyDefender.DataModels;
using Newtonsoft.Json;

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

        [HttpGet]
        //Get all characters
        public List<Character> CharactersGet()
        {
            return _characterService.GetCharacters();
        }

        [HttpPost]
        //Make a Character
        public async Task<Character> CharactersPost()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<CharacterData>(body);

            return _characterService.PostCharacter(model);
        }

        [HttpDelete("{id}")]
        public object DeleteCharacter([FromRoute] int id)
        {
            _characterService.DeleteCharacter(id);

            return Results.Ok();
        }
    }
}
