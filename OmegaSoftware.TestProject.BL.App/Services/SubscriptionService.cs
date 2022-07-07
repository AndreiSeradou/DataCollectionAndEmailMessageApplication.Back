using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Request;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _footballSubscriptionRepository;
        private readonly IQuartzJobService _quartzJobService;
        private readonly IApiRepository _apiRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository footballSubscriptionRepository, IQuartzJobService quartzJobService, IMapper mapper, IApiRepository apiRepository)
        {
            _footballSubscriptionRepository = footballSubscriptionRepository;
            _quartzJobService = quartzJobService;
            _apiRepository = apiRepository;
            _mapper = mapper;
        }

        public ICollection<SubscriptionResponce> GetAllSubscriptions(string userName)
        {
            var subscriptions = _footballSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<SubscriptionResponce>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, SubscriptionRequest model)
        {
            var dalModel = _mapper.Map<Subscription>(model);
            var result = _footballSubscriptionRepository.Create(userName, dalModel);

            if (result)
            {
                var apiModel = _apiRepository.GetByName(model.ApiName);

                await _quartzJobService.CreateJobAsync(email, dalModel, apiModel);
            }
               
            return result;
        }

        public bool Unsubscribe(string userName, SubscriptionRequest model)
        {
            var result = _footballSubscriptionRepository.Delete(userName, model.Id);

            if (result)
            {
                var dalModel = _mapper.Map<Subscription>(model);
                _quartzJobService.DeleteJob(dalModel);
            }

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, SubscriptionRequest model)
        {
            var dalModel = _mapper.Map<Subscription>(model);
            var result = _footballSubscriptionRepository.Update(userName, dalModel);

            if (result)
            {
                var apiModel = _apiRepository.GetByName(model.ApiName);

                await _quartzJobService.CreateJobAsync(email, dalModel, apiModel);
            }

            return result;
        }
    }
}
