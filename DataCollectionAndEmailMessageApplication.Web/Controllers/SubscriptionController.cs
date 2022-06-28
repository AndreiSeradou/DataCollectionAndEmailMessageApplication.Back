using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs.Request;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        [Route("AvailableSubscriptions")]
        public async Task<IActionResult> GetAvailableSubscriptionsAsync()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;

            var result = await _subscriptionService.GetAvailableSubscriptionsAsync(userName);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("MySubscriptions")]
        public async Task<IActionResult> GetMySubscriptionsAsync()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;

            var result = await _subscriptionService.GetMySubscriptionsAsync(userName);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Route("Subscribe")]
        public async Task<IActionResult> SubscribeAsync([FromBody] SubscribeRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;
                var result = _subscriptionService.SubscribeAsync(userName, model);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateSubscription")]
        public async Task<IActionResult> UpdateSubscriptionAsync([FromBody] UpdateSubscriptionRequest model)
        {
            
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;
                var result = await _subscriptionService.UpdateSubscriptionAsync(userName, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("DeleteSubscription")]
        public async Task<IActionResult> DeleteSubscriptionAsync([FromBody] DeleteSubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value;
                var result = await _subscriptionService.DeleteSubscriptionAsync(userName, model);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
