using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.Configuration;
using Quartz;
using System.Text;

namespace OmegaSoftware.TestProject.BL.Domain.Models.Jobs
{
    public class WheatherJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService<WheatherSubscriptionDTOs, string> _apiSenderService;

        public WheatherJob(IEmailSenderService emailSenderService, IApiSenderService<WheatherSubscriptionDTOs, string> apiSenderService)
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
