using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetByName(string userName);
        User GetByEmail(string userEmail);
        bool Create(User model);
        bool Update(User model);
        bool Delete(int id);
    }
}
