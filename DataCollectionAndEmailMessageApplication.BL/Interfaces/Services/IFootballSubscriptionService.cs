using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IFootballSubscriptionService
    {
        ICollection<FootballSubscriptionBLModel> GetAllFootballSubscriptions(string userName);
        Task<bool> SubscribeAsync(string userName, FootballSubscriptionBLModel model);
        Task<bool> UpdateSubscriptionAsync(string userName, FootballSubscriptionBLModel model);
        bool Unsubscribe(string userName, FootballSubscriptionBLModel model);
    }
}
