using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BloggingPlatform.Application.Middleware;
using BloggingPlatform.Domain.Roles;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BloggingPlatform.Api.Controllers
{
    /// <summary>
    /// Login Controller
    /// </summary>
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Login Controller
        /// </summary>
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Login endpoint. Only email and password is required.
        /// </summary>
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest model)
        {

            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            var login = _configuration["Jwt:Login"];
            var password = _configuration["Jwt:Password"];

            if (jwtKey == null || jwtIssuer == null || jwtAudience == null)
            {
                throw new HttpResponseException(500, "Error during authentication.");
            }

            if (model.Email == login && model.Password == password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "UserId"),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role, IdentityUserAccessRoles.USER)
                };

                var token = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
