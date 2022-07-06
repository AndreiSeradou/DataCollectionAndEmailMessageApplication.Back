using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Models;
using Quartz;
using System.Text;


namespace OmegaSoftware.TestProject.BL.Domain.Models.Jobs
{
    public class FootballJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService<FootballSubscription, string> _apiSenderService;

        public FootballJob(IEmailSenderService emailSenderService, IApiSenderService<FootballSubscription, string> apiSenderService)
        {
            _emailSenderService = emailSenderService;
            _apiSenderService = apiSenderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;

            var email = jobData.GetString(ApplicationConfiguration.JobMainParam);

            var apiResult = _apiSenderService.SendOnApi();

            await _emailSenderService.Send(email, apiResult);

        }
    }
}
