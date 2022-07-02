using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IGoogleTranslateSubscriptionService
    {
        ICollection<GoogleTranslateSubscriptionBLModel> GetAllGoogleSubscriptions(string userName);
        Task<bool> SubscribeAsync(string userName, GoogleTranslateSubscriptionBLModel model);
        Task<bool> UpdateSubscriptionAsync(string userName, GoogleTranslateSubscriptionBLModel model);
        bool Unsubscribe(string userName, GoogleTranslateSubscriptionBLModel model);
    }
}
