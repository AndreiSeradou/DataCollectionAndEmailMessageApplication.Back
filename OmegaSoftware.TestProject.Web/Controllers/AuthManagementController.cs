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
using System.Security.Cryptography;
using AutoMapper;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthManagementController(IOptionsMonitor<JwtConfig> optionsMonitor, IUserService userService, IMapper mapper)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _userService = userService;
            _mapper = mapper;
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

                var hashPassword = HashPassword(user.Password);

                if (hashPassword != string.Empty)
                {
                    var newUser = new UserRequest() { Email = user.Email, Name = user.Name, Password = hashPassword, Role = ApplicationConfiguration.UserRole };

                    var isCreated = _userService.CreateUser(newUser);

                    if (isCreated)
                    {
                        var jwtToken = await GenerateJwtToken(newUser);

                        return Ok(jwtToken);
                    }
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
                var existingUser = _userService.GetByEmail(user.Email);

                if(existingUser == null) {
                    return BadRequest(new AuthResponce()
                    {
                        Errors = new List<string> { ApplicationConfiguration.ErrorLogin },
                        Success = false,
                    });
                }

                var verifyResult = VerifyHashedPassword(existingUser.Password, user.Password);

                if (verifyResult)
                {
                    var userToGenerateJWT = _mapper.Map<UserRequest>(existingUser);

                    var jwtToken = await GenerateJwtToken(userToGenerateJWT);

                    return Ok(jwtToken);
                }

                return BadRequest(new AuthResponce()
                {
                    Errors = new List<string> { ApplicationConfiguration.ErrorLogin },
                    Success = false,
                });
            }

            return BadRequest(new AuthResponce()
            {
                Errors = new List<string> { ApplicationConfiguration.ErrorPayload },
                Success = false,
            });
        }

        private async Task<AuthResponce> GenerateJwtToken(UserRequest user)
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


        private static Task<List<Claim>> GetAllValidClaims(UserRequest user)
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

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                return string.Empty;
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                return false;
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }
    }
}
