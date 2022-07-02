using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IQuartzJobService
    {
        Task CreateJobAsync(WheatherSubscriptionBLModel model);
        Task UpdateJobAsync(WheatherSubscriptionBLModel model);
        void DeleteJob(WheatherSubscriptionBLModel model);
        Task CreateJobAsync(FootballSubscriptionBLModel model);
        Task UpdateJobAsync(FootballSubscriptionBLModel model);
        void DeleteJob(FootballSubscriptionBLModel model);
        Task CreateJobAsync(GoogleTranslateSubscriptionBLModel model);
        Task UpdateJobAsync(GoogleTranslateSubscriptionBLModel model);
        void DeleteJob(GoogleTranslateSubscriptionBLModel model);
    }
}
