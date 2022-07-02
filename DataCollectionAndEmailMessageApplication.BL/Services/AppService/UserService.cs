using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.BL.Services.AppService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public bool IsExistihgUserByNameAndEmail(string userName, string userEmail)
        {
            var userByName = _userRepository.GetByName(userName);
            var userByEmil = _userRepository.GetByEmail(userEmail);
            
            if (userByName != null || userByEmil != null)
            {
                return true;
            }

            return false;
        }

        public bool CreateUser(UserBLModel model)
        {
            var dalModel = _mapper.Map<User>(model);
            var result = _userRepository.Create(dalModel);

            return result;
        }

        public bool DeleteUser(UserBLModel model)
        {
            var result = _userRepository.Delete(model.Id);

            return result;
        }

        public ICollection<UserBLModel> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            var result = _mapper.Map<ICollection<UserBLModel>>(users);

            return result;
        }

        public bool UpdateUser(UserBLModel model)
        {
            var dalModel = _mapper.Map<User>(model);
            var result = _userRepository.Update(dalModel);

            return result;
        }
    }
}
