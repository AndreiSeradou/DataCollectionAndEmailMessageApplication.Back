using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.Web.Models.DTOs;

namespace OmegaSoftware.TestProject.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationConfiguration.UserRole)]
    public class WheatherSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<WheatherSubscriptionBLModel> _wheatherSubscriptionService;
        private readonly IMapper _mapper;

        public WheatherSubscriptionController(ISubscriptionService<WheatherSubscriptionBLModel> wheatherSubscriptionService, IMapper mapper)
        {
            _wheatherSubscriptionService = wheatherSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllWheatherSubscriptions")]
        public IActionResult GetAllWheatherSubscriptions()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _wheatherSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map<ICollection<WheatherSubscriptionPLModel>>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe([FromBody] WheatherSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var blModel = _mapper.Map<WheatherSubscriptionBLModel>(model);

                var result = _wheatherSubscriptionService.SubscribeAsync(userName, userEmail, blModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateWheatherSubscription")]
        public async Task<IActionResult> UpdateWheatherSubscriptionAsync([FromBody] WheatherSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var blModel = _mapper.Map<WheatherSubscriptionBLModel>(model);

                var result = await _wheatherSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] WheatherSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var blModel = _mapper.Map<WheatherSubscriptionBLModel>(model);
                var result = _wheatherSubscriptionService.Unsubscribe(userName, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
