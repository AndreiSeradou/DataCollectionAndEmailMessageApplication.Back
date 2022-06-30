using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Models.Jobs
{
    public class GoogleTranslateJob : IJob
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly IApiSenderService _apiSenderService;

        public GoogleTranslateJob(IEmailSenderService emailSenderService, IApiSenderService apiSenderService)
        {
            _emailSenderService = emailSenderService;
            _apiSenderService = apiSenderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var apiResult = _apiSenderService.SendOnGoogleTranslateApi();

            await _emailSenderService.SendEmail(apiResult);
        }
    }
}
