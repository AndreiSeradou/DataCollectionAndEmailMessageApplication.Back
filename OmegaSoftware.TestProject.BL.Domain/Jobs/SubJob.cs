using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;

namespace OmegaSoftware.TestProject.BL.Domain.Models.Jobs
{
    public class SubJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService _apiSenderService;
        private readonly IServiceScopeFactory _scopeFactory;

        public SubJob(IEmailSenderService emailSenderService, IApiSenderService apiSenderService, IServiceScopeFactory scopeFactory)
        {
            _emailSenderService = emailSenderService;
            _apiSenderService = apiSenderService;
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;
            var email = jobData.GetString(ApplicationConfiguration.JobMainParam);
            var subId = jobData.GetInt(ApplicationConfiguration.JobIdParam);
            var url = jobData.GetString(ApplicationConfiguration.JobUrlParam);
            var apiParam = jobData.GetString(ApplicationConfiguration.JobApiParam);
            var keyHeader = jobData.GetString(ApplicationConfiguration.JobKeyHeaderParam);
            var key = jobData.GetString(ApplicationConfiguration.JobKeyParam);
            var hostHeader = jobData.GetString(ApplicationConfiguration.JobHostHeaderParam);
            var host = jobData.GetString(ApplicationConfiguration.JobHostParam);
            var userName = jobData.GetString(ApplicationConfiguration.JobNameParam);

            var apiResult = _apiSenderService.SendOnApi(url, apiParam, keyHeader, key, hostHeader, host);

            await _emailSenderService.Send(email, apiResult);

            using (var scope = _scopeFactory.CreateScope())
            {
                var subscriptionRepository = scope.ServiceProvider.GetRequiredService<ISubscriptionRepository>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var sub = subscriptionRepository.GetById(userName, subId);
                var user = userRepository.GetByName(userName);

                user.NumberOfRunningJobs++;
                sub.LastRunTime = DateTime.UtcNow;

                userRepository.Update(user);
                subscriptionRepository.Update(userName, sub);
            }
        }
    }
}
