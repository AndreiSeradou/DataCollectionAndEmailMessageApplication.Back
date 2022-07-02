using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface IWheatherSubscriptionService
    {
        ICollection<WheatherSubscriptionBLModel> GetAllWheatherSubscriptions(string userName);
        Task<bool> SubscribeAsync(string userName, WheatherSubscriptionBLModel model);
        Task<bool> UpdateSubscriptionAsync(string userName, WheatherSubscriptionBLModel model);
        bool Unsubscribe(string userName, WheatherSubscriptionBLModel model);
    }
}
