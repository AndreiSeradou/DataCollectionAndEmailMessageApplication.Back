using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
{
    public class GoogleTranslateSubscriptionService : ISubscriptionService<GoogleTranslateSubscriptionResponce>
    {
        private readonly ISubscriptionRepository<GoogleTranslateSubscription> _googleTranslateSubscriptionRepository;
        private readonly IQuartzJobService<GoogleTranslateSubscription> _quartzJobService;
        private readonly IMapper _mapper;

        public GoogleTranslateSubscriptionService(ISubscriptionRepository<GoogleTranslateSubscription> googleTranslateSubscriptionRepository, IQuartzJobService<GoogleTranslateSubscription> quartzJobService, IMapper mapper)
        {
            _googleTranslateSubscriptionRepository = googleTranslateSubscriptionRepository;
            _quartzJobService = quartzJobService;
            _mapper = mapper;
        }

        public ICollection<GoogleTranslateSubscriptionResponce> GetAllSubscriptions(string userName)
        {
            var subscriptions = _googleTranslateSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<GoogleTranslateSubscriptionResponce>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, GoogleTranslateSubscriptionResponce model)
        {
            var dalModel = _mapper.Map<GoogleTranslateSubscription>(model);
            var result = _googleTranslateSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(email, dalModel);

            return result;
        }

        public bool Unsubscribe(string userName, GoogleTranslateSubscriptionResponce model)
        {
            var result = _googleTranslateSubscriptionRepository.Delete(userName, model.Id);

            if (result)
            {
                var dalModel = _mapper.Map<GoogleTranslateSubscription>(model);
                _quartzJobService.DeleteJob(dalModel);
            }

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, GoogleTranslateSubscriptionResponce model)
        {
            var dalModel = _mapper.Map<GoogleTranslateSubscription>(model);
            var result = _googleTranslateSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(email, dalModel);

            return result;
        }
    }
}
