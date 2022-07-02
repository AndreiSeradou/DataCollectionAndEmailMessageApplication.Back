using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface IGoogleTranslateSubscriptionRepository
    {
        ICollection<GoogleTranslateSubscription> GetAll(string userName);
        GoogleTranslateSubscription GetById(string userName, int id);
        bool Create(string userName, GoogleTranslateSubscription model);
        bool Update(string userName, GoogleTranslateSubscription model); 
        bool Delete(string userName, int id);
    }
}
