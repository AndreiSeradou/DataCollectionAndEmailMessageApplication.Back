using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
{
    public interface ISubscriptionService<T> where T : class
    {
        ICollection<T> GetAllSubscriptions(string userName);
        Task<bool> SubscribeAsync(string userName, string email, T model);
        Task<bool> UpdateSubscriptionAsync(string userName, string email, T model);
        bool Unsubscribe(string userName, T model);
    }
}
