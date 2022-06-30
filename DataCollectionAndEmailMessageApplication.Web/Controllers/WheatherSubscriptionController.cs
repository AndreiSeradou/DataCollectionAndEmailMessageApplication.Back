using Microsoft.AspNetCore.Mvc;
using Configuration;
using DataCollectionAndEmailMessageApplication.Web.Models.DTOs.Request;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using Quartz;
using Quartz.Spi;
using DataCollectionAndEmailMessageApplication.BL.Models.Jobs;

namespace DataCollectionAndEmailMessageApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WheatherSubscriptionController : ControllerBase
    {
        private readonly IWheatherSubscriptionService _wheatherSubscriptionService;
        private readonly IMapper _mapper;
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }


        public WheatherSubscriptionController(IWheatherSubscriptionService wheatherSubscriptionService, IMapper mapper, ISchedulerFactory schedulerFactory)
        {
            _wheatherSubscriptionService = wheatherSubscriptionService;
            _mapper = mapper;
            _schedulerFactory = schedulerFactory;
        }

        [HttpGet]
        [Route("AllWheatherSubscriptions")]
        public async Task<IActionResult> GetAllWheatherSubscriptionsAsync()
        {
            var my =
            new MyJob(type: typeof(JobReminders), expression: ApplicationConfiguration.Expression);

            // 2, открыть планировщик
        
      
            // 3, создаем триггер
            var trigger =  TriggerBuilder.Create().WithIdentity($"{my.Type.FullName}.trigger").WithCronSchedule(my.Expression).WithDescription(my.Expression).Build();
        
        // 4. Создать задачу
        var jobDetail =  JobBuilder.Create(my.Type).WithIdentity(my.Type.FullName).WithDescription(my.Type.Name).Build();
                
        // 5, Привязать триггеры и задачи к планировщику
        await _scheduler.ScheduleJob(jobDetail, trigger);
            _scheduler.Start();

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
