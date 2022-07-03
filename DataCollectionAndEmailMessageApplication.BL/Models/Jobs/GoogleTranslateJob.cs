using Configuration;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
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
        private readonly IApiSenderService<GoogleTranslateSubscriptionBLModel, string> _apiSenderService;

        public GoogleTranslateJob(IEmailSenderService emailSenderService, IApiSenderService<GoogleTranslateSubscriptionBLModel, string> apiSenderService)
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
