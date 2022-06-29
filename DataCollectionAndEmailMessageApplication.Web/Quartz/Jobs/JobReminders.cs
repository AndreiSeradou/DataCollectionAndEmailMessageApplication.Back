using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
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
            Console.WriteLine("111111");
            await _emailSenderService.SendEmail();
            Console.WriteLine("hello");
        }
    }
}
