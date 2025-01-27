﻿using Microsoft.Extensions.DependencyInjection;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.Jobs;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;
using Quartz;

namespace OmegaSoftware.TestProject.BL.Domain.Services
{
    public class QuartzJobService : IQuartzJobService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private IScheduler _scheduler { get; set; }

        public QuartzJobService(ISchedulerFactory schedulerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _schedulerFactory = schedulerFactory;
            _scopeFactory = serviceScopeFactory;

            //StartAllJods();
        }

        public async Task CreateJobAsync(string email, Subscription subModel, Api apiModel)
        {
            _scheduler = await _schedulerFactory.GetScheduler();

            await _scheduler.Start();

            ITrigger trigger;

            if (subModel.DateStart == null)
            {
                trigger = TriggerBuilder.Create()
                .WithIdentity(subModel.Id.ToString(), subModel.UserName).WithCronSchedule(subModel.CronExpression).StartNow().Build();
            }
            else
            {
                trigger = TriggerBuilder.Create()
              .WithIdentity(subModel.Id.ToString(), subModel.UserName).WithCronSchedule(subModel.CronExpression).StartAt(subModel.DateStart).Build();
            }


            var jobDetail = JobBuilder.Create<SubJob>()
                 .UsingJobData(ApplicationConfiguration.JobMainParam, email)
                 .UsingJobData(ApplicationConfiguration.JobIdParam, subModel.Id)
                 .UsingJobData(ApplicationConfiguration.JobUrlParam, apiModel.Url)
                 .UsingJobData(ApplicationConfiguration.JobApiParam, subModel.ApiParams)
                 .UsingJobData(ApplicationConfiguration.JobKeyHeaderParam, apiModel.ApiKeyHeader)
                 .UsingJobData(ApplicationConfiguration.JobKeyParam, apiModel.ApiKey)
                 .UsingJobData(ApplicationConfiguration.JobHostHeaderParam, apiModel.ApiHostHeader)
                 .UsingJobData(ApplicationConfiguration.JobHostParam, apiModel.ApiHost)
                 .UsingJobData(ApplicationConfiguration.JobNameParam, subModel.UserName)
                 .WithIdentity(subModel.Id.ToString(), subModel.UserName).Build();

            await _scheduler.ScheduleJob(jobDetail, trigger);
        }

        public async Task UpdateJobAsync(string email, Subscription subModel, Api apiModel)
        {
            DeleteJob(subModel);
            await CreateJobAsync(email, subModel, apiModel);
        }

        public void DeleteJob(Subscription model)
        {
            _scheduler.UnscheduleJob(new TriggerKey(model.Id.ToString(), model.UserName));
            _scheduler.DeleteJob(new JobKey(model.Id.ToString(), model.UserName));
        }

        private async void StartAllJods()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var subscriptionRepository = scope.ServiceProvider.GetRequiredService<ISubscriptionRepository>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var apiRepository = scope.ServiceProvider.GetRequiredService<IApiRepository>();

                var subscriptions = subscriptionRepository.GetAll();

                foreach (var sub in subscriptions)
                {
                    var api = apiRepository.GetByName(sub.ApiName);
                    var user = userRepository.GetByName(sub.UserName);

                    await CreateJobAsync(user.Email, sub, api);
                }
            }
        }
    }
}
