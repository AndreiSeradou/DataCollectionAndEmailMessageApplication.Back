using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories
{
    public interface ISubscriptionRepository<T> where T : class
    {
        ICollection<T> GetAll(string userName);
        T GetById(string userName, int id);
        bool Create(string userName, T model);
        bool Update(string userName, T model);
        bool Delete(string userName, int id);
    }
}
