using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<UserBLModel> GetAllUsers();
        bool CreateUser(UserBLModel model);
        bool UpdateUser(UserBLModel model);
        bool DeleteUser(UserBLModel model);
        bool IsExistihgUserByNameAndEmail(string userName, string userEmail);
    }
}
