using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Models;
using Quartz;
using System.Text;

namespace OmegaSoftware.TestProject.BL.Domain.Models.Jobs
{
    public class WheatherJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService<WheatherSubscription, string> _apiSenderService;

        public WheatherJob(IEmailSenderService emailSenderService, IApiSenderService<WheatherSubscription, string> apiSenderService)
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
