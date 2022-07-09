using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
{
    public class ApiService : IApiService
    {
        private readonly IApiRepository _apiRepository;
        private readonly IMapper _mapper;

        public ApiService(IApiRepository apiRepository, IMapper mapper)
        {
            _apiRepository = apiRepository;
            _mapper = mapper;
        }

        public bool Create(ApiRequest model)
        {
            var dalModel = _mapper.Map<Api>(model);
            var result = _apiRepository.Create(dalModel);

            return result;
        }

        public bool Delete(ApiRequest model)
        {
            var result = _apiRepository.Delete(model.Id);

            return result;
        }

        public ICollection<ApiResponce> GetAll()
        {
            var users = _apiRepository.GetAll();
            var result = _mapper.Map<ICollection<ApiResponce>>(users);

            return result;
        }

        public bool Update(ApiRequest model)
        {
            var dalModel = _mapper.Map<Api>(model);
            var result = _apiRepository.Update(dalModel);

            return result;
        }
    }
}
