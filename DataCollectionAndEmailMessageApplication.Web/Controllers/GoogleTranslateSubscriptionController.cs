﻿using AutoMapper;
using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleTranslateSubscriptionController : ControllerBase
    {
        private readonly IGoogleTranslateSubscriptionService _googleTranslateSubscriptionService;
        private readonly IMapper _mapper;

        public GoogleTranslateSubscriptionController(IGoogleTranslateSubscriptionService googleTranslateSubscriptionService, IMapper mapper)
        {
            _googleTranslateSubscriptionService = googleTranslateSubscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("AllGoogleSubscriptions")]
        public IActionResult GetAllGoogleSubscriptionsAsync()
        {
            var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

            var subscriptions = _googleTranslateSubscriptionService.GetAllGoogleSubscriptions(userName);

            var result = _mapper.Map<GoogleTranslateSubscriptionPLModel>(subscriptions);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        [Route("Subscribe")]
        public IActionResult Subscribe([FromBody] GoogleTranslateSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                var blModel = _mapper.Map<GoogleTranslateSubscriptionBLModel>(model);

                var result = _googleTranslateSubscriptionService.SubscribeAsync(userName, blModel);

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpPut]
        [Route("UpdateGoogleSubscription")]
        public async Task<IActionResult> UpdateGoogleSubscription([FromBody] GoogleTranslateSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;

                var blModel = _mapper.Map<GoogleTranslateSubscriptionBLModel>(model);

                var result = await _googleTranslateSubscriptionService.UpdateSubscriptionAsync(userName, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }

        [HttpDelete]
        [Route("Unsubscribe")]
        public IActionResult Unsubscribe([FromBody] GoogleTranslateSubscriptionPLModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirst(ApplicationConfiguration.CustomClaimName)!.Value;
                var blModel = _mapper.Map<GoogleTranslateSubscriptionBLModel>(model);
                var result = _googleTranslateSubscriptionService.Unsubscribe(userName, blModel);

                if (result == false)
                    return NotFound();

                return Ok(result);
            }

            return BadRequest(ApplicationConfiguration.InvalidModel);
        }
    }
}