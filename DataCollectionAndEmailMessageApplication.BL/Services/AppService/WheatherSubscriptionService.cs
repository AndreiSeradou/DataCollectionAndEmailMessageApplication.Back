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

        public ICollection<WheatherSubscriptionBLModel> GetAllWheatherSubscriptions(string userName)
        {
            var subscriptions = _wheatherSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<WheatherSubscriptionBLModel>>(subscriptions);

            return result;
        }

        public bool Subscribe(string userName, WheatherSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<WheatherSubscription>(model);
            _wheatherSubscriptionRepository.Create(dalModel);

            return true;
        }

        public bool Unsubscribe(string userName, int id)
        {
            _wheatherSubscriptionRepository.Delete(id);

            return true;
        }

        public bool UpdateSubscription(string userName, WheatherSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<WheatherSubscription>(model);
            _wheatherSubscriptionRepository.Update(dalModel);

            return true;
        }
    }
}
