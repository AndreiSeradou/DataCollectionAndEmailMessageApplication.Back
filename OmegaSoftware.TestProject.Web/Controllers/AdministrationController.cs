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
        private readonly ISubscriptionService<FootballSubscriptionResponce> _footballSubscriptionService;
        private readonly ISubscriptionService<GoogleTranslateSubscriptionResponce> _googleTranslateSubscriptionService;
        private readonly ISubscriptionService<WheatherSubscriptionResponce> _wheatherSubscriptionService;

        public AdministrationController(IUserService userService, ISubscriptionService<GoogleTranslateSubscriptionResponce> googleTranslateSubscriptionService, ISubscriptionService<WheatherSubscriptionResponce> wheatherSubscriptionService, ISubscriptionService<FootballSubscriptionResponce> footballSubscriptionService)
        {
            _userService = userService;
            _googleTranslateSubscriptionService = googleTranslateSubscriptionService;
            _wheatherSubscriptionService = wheatherSubscriptionService;
            _footballSubscriptionService = footballSubscriptionService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet]
        [Route("weather-subscriptions")]
        public IActionResult GetUserWheatherSubscriptions([FromHeader] string userName)
        {
            var subscriptions = _wheatherSubscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }

        [HttpGet]
        [Route("google-subscriptions")]
        public IActionResult GetUserGoogleSubscriptions([FromHeader] string userName)
        {
            var subscriptions = _googleTranslateSubscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }

        [HttpGet]
        [Route("football-subscriptions")]
        public IActionResult GetUserFootballSubscriptions([FromHeader] string userName)
        {
            var subscriptions = _footballSubscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }

    }
}
