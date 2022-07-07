using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.DAL.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        ICollection<Subscription> GetAll(string userName);
        Subscription GetById(string userName, int id);
        bool Create(string userName, Subscription model);
        bool Update(string userName, Subscription model);
        bool Delete(string userName, int id);
    }
}
