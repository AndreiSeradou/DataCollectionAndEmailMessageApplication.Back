namespace OmegaSoftware.TestProject.BL.Domain.Interfaces.Services
{
    public interface IQuartzJobService<T> where T : class
    {
        Task CreateJobAsync(string email, T model);
        Task UpdateJobAsync(string email, T model);
        void DeleteJob(T model);
    }
}
