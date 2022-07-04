using AutoMapper;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models.DTOs;

namespace OmegaSoftware.TestProject.BL.App.Services.AppService
{
    public class WheatherSubscriptionService : ISubscriptionService<WheatherSubscriptionBLModel>
    {
        private readonly ISubscriptionRepository<WheatherSubscription> _wheatherSubscriptionRepository;
        private readonly IQuartzJobService<WheatherSubscriptionBLModel> _quartzJobService;
        private readonly IMapper _mapper;


        public WheatherSubscriptionService(ISubscriptionRepository<WheatherSubscription> wheatherSubscriptionRepository, IMapper mapper, IQuartzJobService<WheatherSubscriptionBLModel> quartzJobService)
        {
            _wheatherSubscriptionRepository = wheatherSubscriptionRepository;
            _mapper = mapper;
            _quartzJobService = quartzJobService;
        }

        public ICollection<WheatherSubscriptionBLModel> GetAllSubscriptions(string userName)
        {
            var subscriptions = _wheatherSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<WheatherSubscriptionBLModel>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, WheatherSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<WheatherSubscription>(model);
            var result = _wheatherSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(email, model);

            return result;
        }

        public bool Unsubscribe(string userName, WheatherSubscriptionBLModel model)
        {
            var result = _wheatherSubscriptionRepository.Delete(userName, model.Id);

            if (result)
                _quartzJobService.DeleteJob(model);

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, WheatherSubscriptionBLModel model)
        {
            var dalModel = _mapper.Map<WheatherSubscription>(model);
            var result = _wheatherSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(email, model);

            return result;
        }
    }
}
