using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface IApiService
    {
        ICollection<ApiResponce> GetAll();
        bool Create(ApiRequest model);
        bool Update(ApiRequest model);
        bool Delete(ApiRequest model);
    }
}
