using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        ICollection<User> GetAll();
        User GetByName(string userName);
        User GetByEmail(string userEmail);
        void Create(User model);
        void Update(User model);
        void Delete(int id);
    }
}
