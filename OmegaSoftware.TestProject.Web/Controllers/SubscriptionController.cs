using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper  _mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllFootballSubscriptions()
        {
            try
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                var subscriptions = _subscriptionService.GetAll(userName);

                if (subscriptions == null)
                    return NotFound();

                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("subscribe")]
        public IActionResult Subscribe([FromBody] SubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                    var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                    var result = _subscriptionService.SubscribeAsync(userName, userEmail, model);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateFootballSubscriptionAsync([FromBody] SubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                    var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                    var result = await _subscriptionService.UpdateAsync(userName, userEmail, model);

                    if (result == false)
                        return NotFound();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("unsubscribe")]
        public IActionResult Unsubscribe([FromHeader] SubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                    var result = _subscriptionService.Unsubscribe(userName, model);

                    if (result == false)
                        return NotFound();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
