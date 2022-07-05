﻿using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.BL.Domain.Models.Jobs;
using OmegaSoftware.TestProject.Configuration;
using Quartz;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class QuartzFootballJobService : IQuartzJobService<FootballSubscriptionDTOs>
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzFootballJobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task CreateJobAsync(string email, FootballSubscriptionDTOs model)
        {
            _scheduler = await _schedulerFactory.GetScheduler();

            await _scheduler.Start();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(model.Id.ToString(), model.UserName).WithCronSchedule(model.CronParams).Build();

            var jobDetail = JobBuilder.Create<FootballJob>()
                 .UsingJobData(ApplicationConfiguration.JobMainParam, email).WithIdentity(model.Id.ToString(), model.UserName).Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public async Task UpdateJobAsync(string email, FootballSubscriptionDTOs model)
        {
            DeleteJob(model);
            await CreateJobAsync(email, model);
        }

        public void DeleteJob(FootballSubscriptionDTOs model)
        {
            _scheduler.UnscheduleJob(new TriggerKey(model.Id.ToString(), model.UserName));
            _scheduler.DeleteJob(new JobKey(model.Id.ToString(), model.UserName));
        }
    }
}