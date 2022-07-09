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
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuartzJobService _quartzJobService;
        private readonly IApiRepository _apiRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IQuartzJobService quartzJobService, IMapper mapper, IApiRepository apiRepository, IUserRepository userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _quartzJobService = quartzJobService;
            _apiRepository = apiRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public ICollection<SubscriptionResponce> GetAll(string userName)
        {
            var subscriptions = _subscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<SubscriptionResponce>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, SubscriptionRequest model)
        {
            var dalModel = _mapper.Map<Subscription>(model);
            var result = _subscriptionRepository.Create(userName, dalModel);

            if (result)
            {
                var apiModel = _apiRepository.GetByName(model.ApiName);
                var user = _userRepository.GetByName(userName);

                await _quartzJobService.CreateJobAsync(email, dalModel, apiModel);

                user.NumberOfUsesApis++;

                _userRepository.Update(user);
            }
               
            return result;
        }

        public bool Unsubscribe(string userName, SubscriptionRequest model)
        {
            var result = _subscriptionRepository.Delete(userName, model.Id);

            if (result)
            {
                var dalModel = _mapper.Map<Subscription>(model);
                _quartzJobService.DeleteJob(dalModel);
            }

            return result;
        }

        public async Task<bool> UpdateAsync(string userName, string email, SubscriptionRequest model)
        {
            var dalModel = _mapper.Map<Subscription>(model);
            var result = _subscriptionRepository.Update(userName, dalModel);

            if (result)
            {
                var apiModel = _apiRepository.GetByName(model.ApiName);

                await _quartzJobService.CreateJobAsync(email, dalModel, apiModel);
            }

            return result;
        }
    }
}
