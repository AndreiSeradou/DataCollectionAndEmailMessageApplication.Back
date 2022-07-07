using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
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
        private readonly ISubscriptionService _subscriptionService;

        public AdministrationController(IUserService userService, ISubscriptionService subscriptionService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;

        }

        [HttpGet]
        [Route("all-users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet]
        [Route("user-subscriptions")]
        public IActionResult GetUserWheatherSubscriptions([FromHeader] string userName)
        {
            var subscriptions = _subscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }
    }
}
