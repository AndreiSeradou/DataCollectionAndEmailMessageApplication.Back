using AutoMapper;
using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.AdminRole)]
    public class AdministrationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService<FootballSubscriptionBLModel> _footballSubscriptionService;
        private readonly ISubscriptionService<GoogleTranslateSubscriptionBLModel> _googleTranslateSubscriptionService;
        private readonly ISubscriptionService<WheatherSubscriptionBLModel> _wheatherSubscriptionService;
        private readonly IMapper _mapper;

        public AdministrationController(IUserService userService, IMapper mapper, ISubscriptionService<GoogleTranslateSubscriptionBLModel> googleTranslateSubscriptionService, ISubscriptionService<WheatherSubscriptionBLModel> wheatherSubscriptionService, ISubscriptionService<FootballSubscriptionBLModel> footballSubscriptionService)
        {
            _userService = userService;
            _mapper = mapper;
            _googleTranslateSubscriptionService = googleTranslateSubscriptionService;
            _wheatherSubscriptionService = wheatherSubscriptionService;
            _footballSubscriptionService = footballSubscriptionService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            var result = _mapper.Map<ICollection<UserPLModel>>(users);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserWheatherSubscriptions")]
        public IActionResult GetUserWheatherSubscriptions([FromQuery] string userName)
        {
            var subscriptions = _wheatherSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map<ICollection<WheatherSubscriptionPLModel>>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserGoogleSubscriptions")]
        public IActionResult GetUserGoogleSubscriptions([FromQuery] string userName)
        {
            var subscriptions = _googleTranslateSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map<ICollection<GoogleTranslateSubscriptionPLModel>>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserFootballSubscriptions")]
        public IActionResult GetUserFootballSubscriptions([FromQuery] string userName)
        {
            var subscriptions = _footballSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map<ICollection<FootballSubscriptionPLModel>>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
