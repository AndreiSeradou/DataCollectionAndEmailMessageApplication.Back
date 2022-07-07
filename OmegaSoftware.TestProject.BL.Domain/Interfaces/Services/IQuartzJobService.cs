using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.BL.Domain.Interfaces.Services
{
    public interface IQuartzJobService
    {
        Task CreateJobAsync(string email, Subscription subModel, Api apiModel);
        Task UpdateJobAsync(string email, Subscription model, Api apiModel);
        void DeleteJob(Subscription model);
    }
}
