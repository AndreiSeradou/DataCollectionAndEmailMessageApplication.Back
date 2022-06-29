using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Models.DTOs
{
    public class WheatherSubscriptionBLModel : IWheatherSubscriptionService
    {
        public ICollection<WheatherSubscriptionBLModel> GetAllWheatherSubscriptions(string userName)
        {
            throw new NotImplementedException();
        }

        public bool Subscribe(string userName, WheatherSubscriptionBLModel model)
        {
            throw new NotImplementedException();
        }

        public bool Unsubscribe(string userName, int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSubscription(string userName, WheatherSubscriptionBLModel model)
        {
            throw new NotImplementedException();
        }
    }
}
