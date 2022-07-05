using OmegaSoftware.TestProject.BL.App.DTOs.Responce;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<UserResponce> GetAllUsers();
        UserResponce GetByEmail(string userEmail);
        bool CreateUser(UserResponce model);
        bool UpdateUser(UserResponce model);
        bool DeleteUser(UserResponce model);
        bool IsExistihgUserByNameAndEmail(string userName, string userEmail);
    }
}
