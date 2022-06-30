using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.BL.Models.Jobs;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class QuartzJobService : IQuartzJobService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzJobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task CreateJobAsync(WheatherSubscriptionBLModel model)
        {
            _scheduler = await _schedulerFactory.GetScheduler();

            await _scheduler.Start();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(model.Id.ToString(), model.UserId.ToString()).WithCronSchedule(model.CronParams).Build();

            var jobDetail = JobBuilder.Create<WheatherJob>()
                 .UsingJobData("City", model.City).UsingJobData("Date", model.Date).WithIdentity(model.Id.ToString(), model.UserId.ToString()).Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void DeleteJob(WheatherSubscriptionBLModel model)
        {
            _scheduler.UnscheduleJob(new TriggerKey(model.Id.ToString(), model.UserId.ToString()));
            _scheduler.DeleteJob(new JobKey(model.Id.ToString(), model.UserId.ToString()));
        }

        public async Task UpdateJobAsync(WheatherSubscriptionBLModel model)
        {
            DeleteJob(model);
            await CreateJobAsync(model);
        }
    }
}
