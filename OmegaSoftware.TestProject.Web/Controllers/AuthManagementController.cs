using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OmegaSoftware.TestProject.Web.Configuration;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUserService _userService;

        public AuthManagementController(IOptionsMonitor<JwtConfig> optionsMonitor, IUserService userService)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUserByNameAndEmail = _userService.IsExistihgUserByNameAndEmail(user.Name, user.Email);

                if (existingUserByNameAndEmail)
                {
                    return BadRequest(new AuthResponce()
                    {
                        Errors = new List<string>()
                        {
                            ApplicationConfiguration.ErrorName,
                            ApplicationConfiguration.ErrorEmail
                        },
                        Success = false
                    });
                }

                var newUser = new UserResponce() { Email = user.Email, Name  = user.Name, Password = user.Password, Role = ApplicationConfiguration.UserRole};

                var isCreated = _userService.CreateUser(newUser);

                if(isCreated)
                {
                    var jwtToken = await GenerateJwtToken(newUser);
                    
                    return Ok(jwtToken);
                }

                return BadRequest(new AuthResponce()
                {
                    Errors = new List<string> { ApplicationConfiguration.InvalidModel },
                    Success = false,
                });
            }

            return BadRequest(new AuthResponce()
            {
                Errors = new List<string> { ApplicationConfiguration.ErrorPayload },
                Success = false,
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = _userService.GetAllUsers().FirstOrDefault(u => u.Email == user.Email );

                if(existingUser == null) {
                    return BadRequest(new AuthResponce()
                    {
                        Errors = new List<string> { ApplicationConfiguration.ErrorLogin },
                        Success = false,
                    });
                }

                var jwtToken  = await GenerateJwtToken(existingUser);

                return Ok(jwtToken);
            }

            return BadRequest(new AuthResponce()
            {
                Errors = new List<string> { ApplicationConfiguration.ErrorPayload },
                Success = false,
            });
        }

        private async Task<AuthResponce> GenerateJwtToken(UserResponce user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new AuthResponce() {
                Token = jwtToken,
                Success = true,
                Name = user.Name,
                Role = user.Role,
            };
        }


        private static Task<List<Claim>> GetAllValidClaims(UserResponce user)
        {
            var claims = new List<Claim>
            {
                new Claim(ApplicationConfiguration.CustomClaimId, user.Id.ToString()),
                new Claim(ApplicationConfiguration.CustomClaimName, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            return Task.FromResult(claims);
        }
    }
}
