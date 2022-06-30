using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using Quartz;

namespace DataCollectionAndEmailMessageApplication.BL.Models.Jobs
{
    public class WheatherJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService _apiSenderService;

        public WheatherJob(IEmailSenderService emailSenderService, IApiSenderService apiSenderService)
        {
            _emailSenderService = emailSenderService;
            _apiSenderService = apiSenderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;
            var city = jobData.GetString("City");
            var date = jobData.GetString("Date");

            var apiResult = _apiSenderService.SendOnWheatherApi(city, date);

            await _emailSenderService.SendEmail(apiResult);
        }
    }
}
