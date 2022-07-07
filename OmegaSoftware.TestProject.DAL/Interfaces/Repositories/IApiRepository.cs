using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.DAL.Interfaces.Repositories
{
    public interface IApiRepository
    {
        Api GetByName(string apiName);
    }
}
