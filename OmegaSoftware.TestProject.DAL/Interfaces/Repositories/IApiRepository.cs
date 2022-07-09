using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.DAL.Interfaces.Repositories
{
    public interface IApiRepository
    {
        Api GetByName(string apiName);
        Api GetById(int id);
        ICollection<Api> GetAll();
        bool Create(Api model);
        bool Update(Api model);
        bool Delete(int id);
    }
}
