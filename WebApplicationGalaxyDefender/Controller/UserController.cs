using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplicationGalaxyDefender.Model;
using WebApplicationGalaxyDefender.Repository;

namespace WebApplicationGalaxyDefender.Controller
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepos _userRepos;

        public AuthController(IConfiguration config)
        {
            _userRepos = new UserRepos(config);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            bool registered = _userRepos.register(userDto.Username, userDto.Password);

            if (!registered)
            {
                return Conflict("Username already exists.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var token = _userRepos.login(userDto.Username, userDto.Password);

            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { Token = token });
        }
    }
}