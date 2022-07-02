﻿using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
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
    public class QuartzGoogleTranslateJobService : IQuartzJobService<GoogleTranslateSubscriptionBLModel>
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzGoogleTranslateJobService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public async Task CreateJobAsync(string email, GoogleTranslateSubscriptionBLModel model)
        {
            _scheduler = await _schedulerFactory.GetScheduler();

            await _scheduler.Start();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(model.Id.ToString(), model.UserName).WithCronSchedule(model.CronParams).Build();

            var jobDetail = JobBuilder.Create<GoogleTranslateJob>()
                 .UsingJobData("Email", email).WithIdentity(model.Id.ToString(), model.UserName).Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public async Task UpdateJobAsync(string email, GoogleTranslateSubscriptionBLModel model)
        {
            DeleteJob(model);
            await CreateJobAsync(email, model);
        }

        public void DeleteJob(GoogleTranslateSubscriptionBLModel model)
        {
            _scheduler.UnscheduleJob(new TriggerKey(model.Id.ToString(), model.UserName));
            _scheduler.DeleteJob(new JobKey(model.Id.ToString(), model.UserName));
        }
    }
}