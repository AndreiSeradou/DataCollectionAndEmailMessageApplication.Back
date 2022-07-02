using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.BL.Models.Jobs;
using Quartz;

namespace DataCollectionAndEmailMessageApplication.BL.Services.DomainService
{
    public class QuartzWheatherJobService : IQuartzJobService<WheatherSubscriptionBLModel>
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzWheatherJobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task CreateJobAsync(string email, WheatherSubscriptionBLModel model)
        {
            _scheduler = await _schedulerFactory.GetScheduler();

            await _scheduler.Start();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(model.Id.ToString(), model.UserName).WithCronSchedule(model.CronParams).Build();

            var jobDetail = JobBuilder.Create<WheatherJob>()
                 .UsingJobData("City", model.City).UsingJobData("Date", model.Date).UsingJobData("Email", email).WithIdentity(model.Id.ToString(), model.UserName).Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void DeleteJob(WheatherSubscriptionBLModel model)
        {
            _scheduler.UnscheduleJob(new TriggerKey(model.Id.ToString(), model.UserName));
            _scheduler.DeleteJob(new JobKey(model.Id.ToString(), model.UserName));
        }

        public async Task UpdateJobAsync(string email, WheatherSubscriptionBLModel model)
        {
            DeleteJob(model);
            await CreateJobAsync(email, model);
        }
    }
}
