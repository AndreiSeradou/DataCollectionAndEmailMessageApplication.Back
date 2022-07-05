using AutoMapper;
using OmegaSoftware.TestProject.BL.App.DTOs.Responce;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
{
    public class FootballSubscriptionService : ISubscriptionService<FootballSubscriptionResponce>
    {
        private readonly ISubscriptionRepository<FootballSubscription> _footballSubscriptionRepository;
        private readonly IQuartzJobService<FootballSubscription> _quartzJobService;
        private readonly IMapper _mapper;

        public FootballSubscriptionService(ISubscriptionRepository<FootballSubscription> footballSubscriptionRepository, IQuartzJobService<FootballSubscription> quartzJobService, IMapper mapper)
        {
            _footballSubscriptionRepository = footballSubscriptionRepository;
            _quartzJobService = quartzJobService;
            _mapper = mapper;
        }

        public ICollection<FootballSubscriptionResponce> GetAllSubscriptions(string userName)
        {
            var subscriptions = _footballSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<FootballSubscriptionResponce>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, FootballSubscriptionResponce model)
        {
            var dalModel = _mapper.Map<FootballSubscription>(model);
            var result = _footballSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(email, dalModel);

            return result;
        }

        public bool Unsubscribe(string userName, FootballSubscriptionResponce model)
        {
            var result = _footballSubscriptionRepository.Delete(userName, model.Id);

            if (result)
            {
                var dalModel = _mapper.Map<FootballSubscription>(model);
                _quartzJobService.DeleteJob(dalModel);
            }

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, FootballSubscriptionResponce model)
        {
            var dalModel = _mapper.Map<FootballSubscription>(model);
            var result = _footballSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(email, dalModel);

            return result;
        }
    }
}
