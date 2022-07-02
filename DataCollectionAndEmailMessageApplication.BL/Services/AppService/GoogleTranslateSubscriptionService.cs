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
    public class GoogleTranslateSubscriptionService : ISubscriptionService<GoogleTranslateSubscriptionBLModel>
    {
        private readonly ISubscriptionRepository<GoogleTranslateSubscription> _googleTranslateSubscriptionRepository;
        private readonly IQuartzJobService<GoogleTranslateSubscriptionBLModel> _quartzJobService;
        private readonly IMapper _mapper;

        public GoogleTranslateSubscriptionService(ISubscriptionRepository<GoogleTranslateSubscription> googleTranslateSubscriptionRepository, IQuartzJobService<GoogleTranslateSubscriptionBLModel> quartzJobService, IMapper mapper)
        {
            _googleTranslateSubscriptionRepository = googleTranslateSubscriptionRepository;
            _quartzJobService = quartzJobService;
            _mapper = mapper;
        }

        public ICollection<GoogleTranslateSubscriptionBLModel> GetAllSubscriptions(string userName)
        {
            var subscriptions = _googleTranslateSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<GoogleTranslateSubscriptionBLModel>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, GoogleTranslateSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<GoogleTranslateSubscription>(model);
            var result = _googleTranslateSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(email, model);

            return result;
        }

        public bool Unsubscribe(string userName, GoogleTranslateSubscriptionBLModel model)
        {
            var result = _googleTranslateSubscriptionRepository.Delete(userName, model.Id);

            if (result)
                _quartzJobService.DeleteJob(model);

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, GoogleTranslateSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<GoogleTranslateSubscription>(model);
            var result = _googleTranslateSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(email, model);

            return result;
        }
    }
}
