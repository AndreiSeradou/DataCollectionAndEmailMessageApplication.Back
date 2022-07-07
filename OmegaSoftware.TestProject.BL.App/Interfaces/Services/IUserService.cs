using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<UserResponce> GetAllUsers();
        UserResponce GetByEmail(string userEmail);
        bool CreateUser(UserRequest model);
        bool UpdateUser(UserRequest model);
        bool DeleteUser(UserRequest model);
        bool IsExistihgUserByNameAndEmail(string userName, string userEmail);
    }
}
