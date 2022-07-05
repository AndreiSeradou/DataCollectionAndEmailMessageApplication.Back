using AutoMapper;
using OmegaSoftware.TestProject.BL.App.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Interfaces.Services;
using OmegaSoftware.TestProject.BL.Domain.Models.DTOs;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.App.Services
{
    public class FootballSubscriptionService : ISubscriptionService<FootballSubscriptionDTOs>
    {
        private readonly ISubscriptionRepository<FootballSubscription> _footballSubscriptionRepository;
        private readonly IQuartzJobService<FootballSubscriptionDTOs> _quartzJobService;
        private readonly IMapper _mapper;

        public FootballSubscriptionService(ISubscriptionRepository<FootballSubscription> footballSubscriptionRepository, IQuartzJobService<FootballSubscriptionDTOs> quartzJobService, IMapper mapper)
        {
            _footballSubscriptionRepository = footballSubscriptionRepository;
            _quartzJobService = quartzJobService;
            _mapper = mapper;
        }

        public ICollection<FootballSubscriptionDTOs> GetAllSubscriptions(string userName)
        {
            var subscriptions = _footballSubscriptionRepository.GetAll(userName);
            var result = _mapper.Map<ICollection<FootballSubscriptionDTOs>>(subscriptions);

            return result;
        }

        public async Task<bool> SubscribeAsync(string userName, string email, FootballSubscriptionDTOs model)
        {
            var dalModel = _mapper.Map<FootballSubscription>(model);
            var result = _footballSubscriptionRepository.Create(userName, dalModel);

            if (result)
                await _quartzJobService.CreateJobAsync(email, model);

            return result;
        }

        public bool Unsubscribe(string userName, FootballSubscriptionDTOs model)
        {
            var result = _footballSubscriptionRepository.Delete(userName, model.Id);

            if (result)
                _quartzJobService.DeleteJob(model);

            return result;
        }

        public async Task<bool> UpdateSubscriptionAsync(string userName, string email, FootballSubscriptionDTOs model)
        {
            var dalModel = _mapper.Map<FootballSubscription>(model);
            var result = _footballSubscriptionRepository.Update(userName, dalModel);

            if (result)
                await _quartzJobService.UpdateJobAsync(email, model);

            return result;
        }
    }
}
