using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.BL.Interfaces.Services
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
