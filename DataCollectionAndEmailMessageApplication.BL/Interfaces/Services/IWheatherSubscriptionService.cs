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
        bool Subscribe(int userId, WheatherSubscriptionBLModel model);
        bool UpdateSubscription(int userId, WheatherSubscriptionBLModel model);
        bool Unsubscribe(int userId, int id);
    }
}
