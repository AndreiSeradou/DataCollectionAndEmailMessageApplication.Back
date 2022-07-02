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
    public class FootballSubscriptionService : IFootballSubscriptionService
    {
        private readonly IFootballSubscriptionRepository _footballSubscriptionRepository;
        private readonly IQuartzJobService _quartzJobService;
        private readonly IMapper _mapper;

        public FootballSubscriptionService(IFootballSubscriptionRepository footballSubscriptionRepository, IQuartzJobService quartzJobService, IMapper mapper)
        {
            _footballSubscriptionRepository = footballSubscriptionRepository;
            _quartzJobService = quartzJobService;
            _mapper = mapper;
        }

        public ICollection<FootballSubscriptionBLModel> GetAllFootballSubscriptions(string userName)
        {
            var subscriptions = _footballSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<FootballSubscriptionBLModel>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, FootballSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<FootballSubscription>(model);
            var result = _footballSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(model);

            return result;
        }

        public bool Unsubscribe(string userName, FootballSubscriptionBLModel model)
        {
            var result = _footballSubscriptionRepository.Delete(userName, model.Id);

            if (result)
                _quartzJobService.DeleteJob(model);

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, FootballSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<FootballSubscription>(model);
            var result = _footballSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(model);

            return result;
        }
    }
}
