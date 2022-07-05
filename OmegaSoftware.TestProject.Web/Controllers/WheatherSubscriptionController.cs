using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class WheatherSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<WheatherSubscriptionResponce> _wheatherSubscriptionService;
        private readonly IMapper _mapper;

        public WheatherSubscriptionController(ISubscriptionService<WheatherSubscriptionResponce> wheatherSubscriptionService, IMapper mapper)
        {
            _wheatherSubscriptionService = wheatherSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllWheatherSubscriptions()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _wheatherSubscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }


        [HttpPost]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody] WheatherSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var mapModel = _mapper.Map<WheatherSubscriptionResponce>(model);

                var result = _wheatherSubscriptionService.SubscribeAsync(userName, userEmail, mapModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateWheatherSubscriptionAsync([FromBody] WheatherSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var mapModel = _mapper.Map<WheatherSubscriptionResponce>(model);

                var result = await _wheatherSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, mapModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] WheatherSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                var mapModel = _mapper.Map<WheatherSubscriptionResponce>(model);

                var result = _wheatherSubscriptionService.Unsubscribe(userName, mapModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
