using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;

namespace OmegaSoftware.TestProject.BL.App.Interfaces.Services
{
    public interface IUserService
    {
        ICollection<UserDTOs> GetAllUsers();
        bool CreateUser(UserDTOs model);
        bool UpdateUser(UserDTOs model);
        bool DeleteUser(UserDTOs model);
        bool IsExistihgUserByNameAndEmail(string userName, string userEmail);
    }
}
