using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<UserResponce> GetAll();
        UserResponce GetByEmail(string userEmail);
        bool Create(UserRequest model);
        bool Update(UserRequest model);
        bool Delete(UserRequest model);
        bool IsExisting(string userName, string userEmail);
    }
}
