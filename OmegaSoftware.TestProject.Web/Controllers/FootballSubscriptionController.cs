using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class FootballSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<FootballSubscriptionDTOs> _footballSubscriptionService;

        public FootballSubscriptionController(ISubscriptionService<FootballSubscriptionDTOs> footballSubscriptionService)
        {
            _footballSubscriptionService = footballSubscriptionService;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllFootballSubscriptions()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _footballSubscriptionService.GetAllSubscriptions(userName);

            if (subscriptions == null)
                return NotFound();

            return Ok(subscriptions);
        }


        [HttpPost]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody] FootballSubscriptionDTOs model)
        {
            if (ModelState.IsValid)
            {                
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var result = _footballSubscriptionService.SubscribeAsync(userName, userEmail, model);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateFootballSubscriptionAsync([FromBody] FootballSubscriptionDTOs model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var result = await _footballSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] FootballSubscriptionDTOs model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var result = _footballSubscriptionService.Unsubscribe(userName, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
