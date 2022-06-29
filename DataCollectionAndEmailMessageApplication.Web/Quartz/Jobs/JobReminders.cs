using Quartz;

namespace DataCollectionAndEmailMessageApplication.Web.Quartz.Jobs
{
    public class JobReminders : IJob
    {
        private readonly IEmailSenderService _emailSenderService;

        public JobReminders(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _emailSenderService.SendEmail();
        }
    }
}
