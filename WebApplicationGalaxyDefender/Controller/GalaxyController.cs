using Microsoft.AspNetCore.Mvc;
using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Service;
using WebApplicationGalaxyDefender.DataModels;
using Newtonsoft.Json;

namespace WebApplicationGalaxyDefender.Controller
{
    [Route("api/galaxies")]
    [ApiController]
    public class GalaxyController : ControllerBase
    {
        private GalaxyService _galaxyService;

        public GalaxyController(GalaxyService galaxyService)
        {
            _galaxyService = galaxyService;
        }

        [HttpGet]
        //Get all galaxies
        public List<Galaxy> GalaxiesGet()
        {
            return _galaxyService.GetGalaxies();
        }

        [HttpGet("{id}")]
        //Gets character by id
        public Galaxy GalaxyGetById(int id)
        {
            return _galaxyService.GetGalaxyById(id);
        }

        [HttpPost]
        //Make a Character
        public async Task<Galaxy> GalaxyPost()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<GalaxyData>(body);

            return _galaxyService.PostGalaxy(model);
        }

        [HttpDelete("{id}")]
        //Deletes character
        public object DeleteGalaxy([FromRoute] int id)
        {
            _galaxyService.DeleteGalaxy(id);

            return Results.Ok();
        }

        [HttpPut]
        //Udpates character
        public async Task<Galaxy> GalaxyPut()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<GalaxyData>(body);

            return _galaxyService.PutGalaxy(model);
        }
    }
}
