using Microsoft.AspNetCore.Mvc;
using Configuration;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs.Request;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WheatherSubscriptionController : ControllerBase
    {
        private readonly IWheatherSubscriptionService _wheatherSubscriptionService;
        public WheatherSubscriptionController(IWheatherSubscriptionService wheatherSubscriptionService)
        {
            _wheatherSubscriptionService = wheatherSubscriptionService;
        }

        [HttpGet]
        [Route("AllWheatherSubscriptions")]
        public async Task<IActionResult> GetAllWheatherSubscriptions()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;

            var result = await _wheatherSubscriptionService.GetAllWheatherSubscriptions(userName);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [Route("Subscribe")]
        public async Task<IActionResult> SubscribeAsync([FromBody] WheatherSubscribeRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;
                var result = _wheatherSubscriptionService.Subscribe(userName, model);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateWheatherSubscription")]
        public async Task<IActionResult> UpdateWheatherSubscriptionAsync([FromBody] UpdateWheatherSubscriptionRequest model)
        {
            
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;
                var result = await _wheatherSubscriptionService.UpdateSubscriptionAsync(userName, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("DeleteWheatherSubscription")]
        public async Task<IActionResult> DeleteSubscriptionAsync([FromBody] DeleteWheatherSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;
                var result = await _wheatherSubscriptionService.DeleteWheatherSubscriptionAsync(userName, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
