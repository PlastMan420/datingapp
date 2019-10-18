using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using datingapp.Data;
using datingapp.Dtos;
using datingapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace datingapp.Controllers
{
    [Route("api/[controller]")] //in here, the [controller] is gonna be replaced by "Auth" in the class name.
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) //placeholder parameters
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower(); //lowercase names only

            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");
            //Create user
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

             //route of the new data we created.
            return StatusCode(201); //placeholder return
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };
            //Create security key.
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));
            //use that security key as part of the credentials, and encrypt using sha512.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //token structure.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            //create token based on token description specified in "tokenDescriptor"
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Write token into a response and send as an object
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}