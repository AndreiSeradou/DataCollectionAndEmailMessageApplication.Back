using AutoMapper;
using DataCollectionAndEmailMessageApplication.BL.Interfaces.Services;
using DataCollectionAndEmailMessageApplication.BL.Models.DTOs;
using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;

namespace DataCollectionAndEmailMessageApplication.BL.Services.AppService
{
    public class WheatherSubscriptionService : IWheatherSubscriptionService
    {
        private readonly IWheatherSubscriptionRepository _wheatherSubscriptionRepository;
        private readonly IQuartzJobService _quartzJobService;
        private readonly IMapper _mapper;


        public WheatherSubscriptionService(IWheatherSubscriptionRepository wheatherSubscriptionRepository, IMapper mapper, IQuartzJobService quartzJobService)
        {
            _wheatherSubscriptionRepository = wheatherSubscriptionRepository;
            _mapper = mapper;
            _quartzJobService = quartzJobService;
        }

        public ICollection<WheatherSubscriptionBLModel> GetAllWheatherSubscriptions(int userId)
        {
            var subscriptions = _wheatherSubscriptionRepository.GetAll(userId).Where(s => s.UserId == userId);
            var result = _mapper.Map<ICollection<WheatherSubscriptionBLModel>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(int userId, WheatherSubscriptionBLModel model)
        {
            try
            {
                var dalModel = _mapper.Map<WheatherSubscription>(model);
                
                _wheatherSubscriptionRepository.Create(dalModel);
                await _quartzJobService.CreateJobAsync(model);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Unsubscribe(WheatherSubscriptionBLModel model)
        {
            try
            {
                _wheatherSubscriptionRepository.Delete(model.Id);
                _quartzJobService.DeleteJob(model);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateSubscriptionAsync(int userId, WheatherSubscriptionBLModel model)
        {
            try
            {
                var dalModel = _mapper.Map<WheatherSubscription>(model);

                _wheatherSubscriptionRepository.Update(dalModel);
                await _quartzJobService.UpdateJobAsync(model);
            }
            catch
            {   
                return false;
            }

            return true;
        }
    }
}
