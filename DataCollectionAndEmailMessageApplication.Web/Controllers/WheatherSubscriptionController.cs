using Microsoft.AspNetCore.Mvc;
using Configuration;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs.Request;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WheatherSubscriptionController : ControllerBase
    {
        private readonly IWheatherSubscriptionService _wheatherSubscriptionService;
        private readonly IMapper _mapper;

        public WheatherSubscriptionController(IWheatherSubscriptionService wheatherSubscriptionService, IMapper mapper)
        {
            _wheatherSubscriptionService = wheatherSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("AllWheatherSubscriptions")]
        public IActionResult GetAllWheatherSubscriptions()
        {
            var userId = Convert.ToInt32(User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value);

            var result = _wheatherSubscriptionService.GetAllWheatherSubscriptions(userId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe([FromBody] WheatherSubscribeRequest model)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value);
                var plModel = _mapper.Map<WheatherSubscriptionBLModel>(model);

                var result = _wheatherSubscriptionService.Subscribe(userId, plModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateWheatherSubscription")]
        public IActionResult UpdateWheatherSubscription([FromBody] UpdateWheatherSubscriptionRequest model)
        {
            
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value);
                var plModel = _mapper.Map<WheatherSubscriptionBLModel>(model);

                var result = _wheatherSubscriptionService.UpdateSubscription(userId, plModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(User.FindFirst(ApplicationConfiguration.CustomClaim)!.Value);
                var result = _wheatherSubscriptionService.Unsubscribe(userId, id);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}
