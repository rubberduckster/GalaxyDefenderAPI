using Microsoft.AspNetCore.Mvc;
using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Service;
using WebApplicationGalaxyDefender.DataModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        //Get all characters
        public List<Character> CharactersGet([FromQuery] int? galaxy)
        {
            return _characterService.GetCharacters(galaxy);
        }

        [HttpGet("{id}")]
        //Gets character by id
        public Character CharactersGetById(int id)
        {
            return _characterService.GetCharacterById(id);
        }

        [HttpGet("{charId}/path")]
        public CharacterPaths[] GetCharacterPaths(int charId)
        {
            return _characterService.GetCharacterPaths(charId);
        }

        //[Authorize]
        [HttpPost]
        //Make a Character
        public async Task<Character> CharactersPost()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<CharacterData>(body);

            return _characterService.PostCharacter(model);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        //Deletes character
        public object DeleteCharacter([FromRoute] int id)
        {
            _characterService.DeleteCharacter(id);

            return Results.Ok();
        }

        //[Authorize]
        [HttpPut]
        //Udpates character
        public async Task<Character> CharactersPut()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<CharacterData>(body);

            return _characterService.PutCharacter(model);
        }
    }
}
