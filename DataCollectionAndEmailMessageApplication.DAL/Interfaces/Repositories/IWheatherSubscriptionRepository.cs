using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface IWheatherSubscriptionRepository
    {
        ICollection<WheatherSubscription> GetAll(string userName);
        WheatherSubscription GetById(string userName ,int id);
        void Create(WheatherSubscription model);
        void Update(WheatherSubscription model);
        void Delete(int id);
    }
}
