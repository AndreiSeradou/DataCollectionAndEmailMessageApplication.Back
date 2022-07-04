namespace OmegaSoftware.TestProject.DAL.Interfaces.Repositories
{
    public interface ISubscriptionRepository<T> where T : class
    {
        ICollection<T> GetAll(string userName);
        T GetById(string userName, int id);
        bool Create(string userName, T model);
        bool Update(string userName, T model);
        bool Delete(string userName, int id);
    }
}
