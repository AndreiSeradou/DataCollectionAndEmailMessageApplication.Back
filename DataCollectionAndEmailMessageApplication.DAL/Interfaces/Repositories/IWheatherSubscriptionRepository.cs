using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface IWheatherSubscriptionRepository
    {
        ICollection<WheatherSubscription> GetAll(string userName);
        WheatherSubscription GetById(string userName ,int id);
        bool Create(string userName, WheatherSubscription model);
        bool Update(string userName, WheatherSubscription model);
        bool Delete(string userName, int id);
    }
}
