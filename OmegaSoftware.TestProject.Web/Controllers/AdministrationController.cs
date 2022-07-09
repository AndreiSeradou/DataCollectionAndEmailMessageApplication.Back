using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.AdminRole)]
    public class AdministrationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApiService _apiService;

        public AdministrationController(IUserService userService, IApiService apiService)
        {
            _userService = userService;
            _apiService = apiService;
        }
    }
}
