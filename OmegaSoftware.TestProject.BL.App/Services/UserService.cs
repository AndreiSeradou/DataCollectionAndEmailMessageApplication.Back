﻿using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
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

        public bool IsExisting(string userName, string userEmail)
        {
            var userByName = _userRepository.GetByName(userName);
            var userByEmil = _userRepository.GetByEmail(userEmail);

            if (userByName != null || userByEmil != null)
            {
                return true;
            }

            return false;
        }

        public bool Create(UserRequest model)
        {
            var dalModel = _mapper.Map<User>(model);
            var result = _userRepository.Create(dalModel);

            return result;
        }

        public bool Delete(UserRequest model)
        {
            var result = _userRepository.Delete(model.Id);

            return result;
        }

        public ICollection<UserResponce> GetAll()
        {
            var users = _userRepository.GetAll();
            var result = _mapper.Map<ICollection<UserResponce>>(users);

            return result;
        }

        public bool Update(UserRequest model)
        {
            var dalModel = _mapper.Map<User>(model);
            var result = _userRepository.Update(dalModel);

            return result;
        }

        public UserResponce GetByEmail(string userEmail)
        {
            var userByEmil = _userRepository.GetByEmail(userEmail);

            var result = _mapper.Map<UserResponce>(userByEmil);

            return result;
        }
    }
}
