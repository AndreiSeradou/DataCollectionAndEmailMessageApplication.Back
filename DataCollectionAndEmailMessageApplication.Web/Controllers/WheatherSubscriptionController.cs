using Microsoft.AspNetCore.Mvc;
using Configuration;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Route("AllWheatherSubscriptions")]
        public IActionResult GetAllWheatherSubscriptionsAsync()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _wheatherSubscriptionService.GetAllSubscriptions(userName);

            var result = _mapper.Map<WheatherSubscriptionPLModel>(subscriptions);

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

                var blModel = _mapper.Map<WheatherSubscriptionBLModel>(model);

                var result = _wheatherSubscriptionService.SubscribeAsync(userName, blModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateWheatherSubscription")]
        public async Task<IActionResult> UpdateWheatherSubscription([FromBody] WheatherSubscriptionPLModel model)
        {     
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                var blModel = _mapper.Map<WheatherSubscriptionBLModel>(model);

                var result = await _wheatherSubscriptionService.UpdateSubscriptionAsync(userName, blModel);

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
