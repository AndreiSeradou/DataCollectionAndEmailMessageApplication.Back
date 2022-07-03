using AutoMapper;
using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class GoogleTranslateSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<GoogleTranslateSubscriptionBLModel> _googleTranslateSubscriptionService;
        private readonly IMapper _mapper;

        public GoogleTranslateSubscriptionController(ISubscriptionService<GoogleTranslateSubscriptionBLModel> googleTranslateSubscriptionService, IMapper mapper)
        {
            _googleTranslateSubscriptionService = googleTranslateSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllGoogleSubscriptions")]
        public IActionResult GetAllGoogleSubscriptions()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _googleTranslateSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map< ICollection<GoogleTranslateSubscriptionPLModel>>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe([FromBody] GoogleTranslateSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var blModel = _mapper.Map<GoogleTranslateSubscriptionBLModel>(model);

                var result = _googleTranslateSubscriptionService.SubscribeAsync(userName, userEmail, blModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateGoogleSubscription")]
        public async Task<IActionResult> UpdateGoogleSubscriptionAsync([FromBody] GoogleTranslateSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var blModel = _mapper.Map<GoogleTranslateSubscriptionBLModel>(model);

                var result = await _googleTranslateSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] GoogleTranslateSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var blModel = _mapper.Map<GoogleTranslateSubscriptionBLModel>(model);
                var result = _googleTranslateSubscriptionService.Unsubscribe(userName, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
