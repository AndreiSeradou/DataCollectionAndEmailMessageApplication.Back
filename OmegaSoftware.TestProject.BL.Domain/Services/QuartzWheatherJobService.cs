using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Models;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.BL.Domain.Models.Jobs;
using Quartz;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class QuartzWheatherJobService : IQuartzJobService<WheatherSubscription>
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzWheatherJobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task CreateJobAsync(string email, WheatherSubscription model)
        {
            _scheduler = await _schedulerFactory.GetScheduler();

            await _scheduler.Start();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(model.Id.ToString(), model.UserName).WithCronSchedule(model.CronParams).Build();

            var jobDetail = JobBuilder.Create<WheatherJob>()
                 .UsingJobData(ApplicationConfiguration.WheatherParam1, model.City).UsingJobData(ApplicationConfiguration.WheatherParam2, model.Date).UsingJobData(ApplicationConfiguration.JobMainParam, email).WithIdentity(model.Id.ToString(), model.UserName).Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public void DeleteJob(WheatherSubscription model)
        {
            _scheduler.UnscheduleJob(new TriggerKey(model.Id.ToString(), model.UserName));
            _scheduler.DeleteJob(new JobKey(model.Id.ToString(), model.UserName));
        }

        public async Task UpdateJobAsync(string email, WheatherSubscription model)
        {
            DeleteJob(model);
            await CreateJobAsync(email, model);
        }
    }
}
