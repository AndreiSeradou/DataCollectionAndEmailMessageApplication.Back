using AutoMapper;
using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballSubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService<FootballSubscriptionBLModel> _footballSubscriptionService;
        private readonly IMapper _mapper;

        public FootballSubscriptionController(ISubscriptionService<FootballSubscriptionBLModel> footballSubscriptionService, IMapper mapper)
        {
            _footballSubscriptionService = footballSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("AllFootballSubscriptions")]
        public IActionResult GetAllFootballSubscriptionsAsync()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _footballSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map<FootballSubscriptionPLModel>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe([FromBody] FootballSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {                
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var blModel = _mapper.Map<FootballSubscriptionBLModel>(model);

                var result = _footballSubscriptionService.SubscribeAsync(userName, userEmail, blModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateFootballSubscription")]
        public async Task<IActionResult> UpdateFootballSubscription([FromBody] FootballSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var userEmail = User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;

                var blModel = _mapper.Map<FootballSubscriptionBLModel>(model);

                var result = await _footballSubscriptionService.UpdateSubscriptionAsync(userName, userEmail, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] FootballSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var blModel = _mapper.Map<FootballSubscriptionBLModel>(model);
                var result = _footballSubscriptionService.Unsubscribe(userName, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
