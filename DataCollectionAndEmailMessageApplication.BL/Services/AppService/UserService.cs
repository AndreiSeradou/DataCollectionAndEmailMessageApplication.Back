using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool CreateUser(UserBLModel model)
        {
            try
            {
                var dalModel = _mapper.Map<User>(model);

                _userRepository.Create(dalModel);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool DeleteUser(UserBLModel model)
        {
            try
            {
                _userRepository.Delete(model.Id);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public ICollection<UserBLModel> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            var result = _mapper.Map<ICollection<UserBLModel>>(users);

            return result;
        }

        public bool UpdateUser(UserBLModel model)
        {
            try
            {
                var dalModel = _mapper.Map<User>(model);

                _userRepository.Update(dalModel);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
