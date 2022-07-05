using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class WheatherSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<WheatherSubscriptionDTOs> _wheatherSubscriptionService;

        public WheatherSubscriptionController(ISubscriptionService<WheatherSubscriptionDTOs> wheatherSubscriptionService)
        {
            _wheatherSubscriptionService = wheatherSubscriptionService;
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
        public IActionResult Subscribe([FromBody] WheatherSubscriptionDTOs model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var result = _wheatherSubscriptionService.SubscribeAsync(userName, userEmail, model);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateWheatherSubscriptionAsync([FromBody] WheatherSubscriptionDTOs model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var result = await _wheatherSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] WheatherSubscriptionDTOs model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var result = _wheatherSubscriptionService.Unsubscribe(userName, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
