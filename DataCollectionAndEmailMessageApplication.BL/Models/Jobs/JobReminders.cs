using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using Quartz;

namespace DataCollectionAndEmailMessageApplication.BL.Models.Jobs
{
    public class JobReminders : IJob
    {
        private readonly IEmailSenderService _emailSenderService;

        public JobReminders(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        public  Task Execute(IJobExecutionContext context)
        {

            //await _emailSenderService.SendEmail();
            return Task.Run(() =>
            {
                Console.WriteLine("1111");
            });
        }
    }
}
