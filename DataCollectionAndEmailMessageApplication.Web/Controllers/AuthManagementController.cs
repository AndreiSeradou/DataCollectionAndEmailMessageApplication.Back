using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineLibrary.DataAccessLayer.Entities;
using OnlineLibrary.PresentationLayer.Configuration;
using OnlineLibrary.PresentationLayer.Models.DTOs.Requests;
using OnlineLibrary.Configuration.GeneralConfiguration;
using OnlineLibrary.PresentationLayer.Models.DTOs.Responses;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthManagementController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(GeneralConfiguration.LibrarianRole));
                await _roleManager.CreateAsync(new IdentityRole(GeneralConfiguration.UserRole));
               
                var existingUserByName = await _userManager.FindByNameAsync(user.Username);
                var existingUserByEmail = await _userManager.FindByEmailAsync(user.Email);


                if (existingUserByName != null || existingUserByEmail != null)
                {
                    return BadRequest(new AuthResult(){
                            Errors = new List<string>() {
                                GeneralConfiguration.ErrorName,
                                GeneralConfiguration.ErrorEmail
                            },
                            Success = false
                    });
                }

                var newUser = new User() { Email = user.Email, UserName = user.Username};
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if(isCreated.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, GeneralConfiguration.UserRole);
                    var role = await _userManager.GetRolesAsync(newUser);

                    var jwtToken = await GenerateJwtToken( newUser);

                    return Ok(jwtToken);
                } else {
                    return BadRequest(new AuthResult(){
                            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                            Success = false,
                    });
                }
            }

            return BadRequest(new AuthResult(){
                    Errors = new List<string>() {
                        GeneralConfiguration.ErrorPayload
                    },
                    Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser == null) {
                        return BadRequest(new AuthResult(){
                            Errors = new List<string>() {
                                GeneralConfiguration.ErrorLogin
                            },
                            Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(!isCorrect) {
                      return BadRequest(new AuthResult(){
                            Errors = new List<string>() {
                                GeneralConfiguration.ErrorLogin
                            },
                            Success = false
                    });
                }

                var jwtToken  = await GenerateJwtToken(existingUser);

                return Ok(jwtToken);
            }

            return BadRequest(new AuthResult(){
                    Errors = new List<string>() {
                        GeneralConfiguration.ErrorPayload
                    },
                    Success = false
            });
        }

        private async Task<AuthResult> GenerateJwtToken(User user)
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
            var role = await _userManager.GetRolesAsync(user);



            return new AuthResult() {
                Token = jwtToken,
                Success = true,
                Name = user.UserName,
                Role = role.FirstOrDefault()
            };
        }


        private async Task<List<Claim>> GetAllValidClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(GeneralConfiguration.CustomClaim, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach(var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if(role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach(var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }
    }
}
