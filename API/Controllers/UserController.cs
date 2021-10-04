using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] string username)
        {
            if (username == "admin")
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return BadRequest("Invalid username or password.");
        }
    }
}