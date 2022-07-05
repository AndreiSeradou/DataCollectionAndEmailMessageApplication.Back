using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
{
    public class WheatherSubscriptionService : ISubscriptionService<WheatherSubscriptionResponce>
    {
        private readonly ISubscriptionRepository<WheatherSubscription> _wheatherSubscriptionRepository;
        private readonly IQuartzJobService<WheatherSubscription> _quartzJobService;
        private readonly IMapper _mapper;


        public WheatherSubscriptionService(ISubscriptionRepository<WheatherSubscription> wheatherSubscriptionRepository, IMapper mapper, IQuartzJobService<WheatherSubscription> quartzJobService)
        {
            _wheatherSubscriptionRepository = wheatherSubscriptionRepository;
            _mapper = mapper;
            _quartzJobService = quartzJobService;
        }

        public ICollection<WheatherSubscriptionResponce> GetAllSubscriptions(string userName)
        {
            var subscriptions = _wheatherSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<WheatherSubscriptionResponce>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, WheatherSubscriptionResponce model)
        {
            var dalModel = _mapper.Map<WheatherSubscription>(model);
            var result = _wheatherSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(email, dalModel);

            return result;
        }

        public bool Unsubscribe(string userName, WheatherSubscriptionResponce model)
        {
            var result = _wheatherSubscriptionRepository.Delete(userName, model.Id);

            if (result)
            {
                var dalModel = _mapper.Map<WheatherSubscription>(model);
                _quartzJobService.DeleteJob(dalModel);
            }

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, WheatherSubscriptionResponce model)
        {
            var dalModel = _mapper.Map<WheatherSubscription>(model);
            var result = _wheatherSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(email, dalModel);

            return result;
        }
    }
}
