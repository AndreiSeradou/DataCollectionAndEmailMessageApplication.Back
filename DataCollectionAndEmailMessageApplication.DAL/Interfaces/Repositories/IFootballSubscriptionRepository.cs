using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface IFootballSubscriptionRepository
    {
        ICollection<FootballSubscription> GetAll(string userName);
        FootballSubscription GetById(string userName, int id);
        bool Create(string userName, FootballSubscription model);
        bool Update(string userName, FootballSubscription model);
        bool Delete(string userName, int id);
    }
}
