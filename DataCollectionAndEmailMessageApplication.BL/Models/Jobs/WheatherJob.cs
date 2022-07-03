using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using Quartz;

namespace DataCollectionAndEmailMessageApplication.BL.Models.Jobs
{
    public class WheatherJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService<WheatherSubscriptionBLModel, string> _apiSenderService;

        public WheatherJob(IEmailSenderService emailSenderService, IApiSenderService<WheatherSubscriptionBLModel, string> apiSenderService)
        {
            _emailSenderService = emailSenderService;
            _apiSenderService = apiSenderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;

            var email = jobData.GetString(ApplicationConfiguration.JobMainParam);
            var city = jobData.GetString(ApplicationConfiguration.WheatherParam1);
            var date = jobData.GetString(ApplicationConfiguration.WheatherParam2);

            var apiResult = _apiSenderService.SendOnApi(new List<string> { city, date });

            await _emailSenderService.Send(email, apiResult);
        }
    }
}
