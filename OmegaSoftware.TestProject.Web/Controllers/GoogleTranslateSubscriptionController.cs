using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class GoogleTranslateSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<GoogleTranslateSubscriptionResponce> _googleTranslateSubscriptionService;
        private readonly IMapper _mapper;

        public GoogleTranslateSubscriptionController(ISubscriptionService<GoogleTranslateSubscriptionResponce> googleTranslateSubscriptionService, IMapper mapper)
        {
            _googleTranslateSubscriptionService = googleTranslateSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllGoogleSubscriptions()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _googleTranslateSubscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }


        [HttpPost]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody] GoogleTranslateSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var mapModel = _mapper.Map<GoogleTranslateSubscriptionResponce>(model);

                var result = _googleTranslateSubscriptionService.SubscribeAsync(userName, userEmail, mapModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateGoogleSubscriptionAsync([FromBody] GoogleTranslateSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var mapModel = _mapper.Map<GoogleTranslateSubscriptionResponce>(model);

                var result = await _googleTranslateSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, mapModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] GoogleTranslateSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                var mapModel = _mapper.Map<GoogleTranslateSubscriptionResponce>(model);

                var result = _googleTranslateSubscriptionService.Unsubscribe(userName, mapModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
