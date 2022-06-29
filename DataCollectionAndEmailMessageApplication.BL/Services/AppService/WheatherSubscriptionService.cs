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
        private readonly IMapper _mapper;


        public WheatherSubscriptionService(IWheatherSubscriptionRepository wheatherSubscriptionRepository, IMapper mapper)
        {
            _wheatherSubscriptionRepository = wheatherSubscriptionRepository;
            _mapper = mapper;
        }

        public ICollection<WheatherSubscriptionBLModel> GetAllWheatherSubscriptions(int userId)
        {
            var subscriptions = _wheatherSubscriptionRepository.GetAll(userId);
            var result = _mapper.Map<ICollection<WheatherSubscriptionBLModel>>(subscriptions);

            return result;
        }

        public bool Subscribe(int userId, WheatherSubscriptionBLModel model)
        {
            try
            {
                var dalModel = _mapper.Map<WheatherSubscription>(model);
                _wheatherSubscriptionRepository.Create(dalModel);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Unsubscribe(int userId, int id)
        {
            try
            {
                _wheatherSubscriptionRepository.Delete(id);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool UpdateSubscription(int userId, WheatherSubscriptionBLModel model)
        {
            try
            {
                var dalModel = _mapper.Map<WheatherSubscription>(model);
                _wheatherSubscriptionRepository.Update(dalModel);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
