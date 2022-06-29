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
        bool Subscribe(string userName, WheatherSubscriptionBLModel model);
        bool UpdateSubscription(string userName, WheatherSubscriptionBLModel model);
        bool Unsubscribe(string userName, int id);
    }
}
