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
        ICollection<WheatherSubscriptionBLModel> GetAllWheatherSubscriptions(int userId);
        Task<bool> SubscribeAsync(int userId, WheatherSubscriptionBLModel model);
        Task<bool> UpdateSubscriptionAsync(int userId, WheatherSubscriptionBLModel model);
        bool Unsubscribe(WheatherSubscriptionBLModel model);
    }
}
