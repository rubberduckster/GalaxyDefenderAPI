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

    }
}
