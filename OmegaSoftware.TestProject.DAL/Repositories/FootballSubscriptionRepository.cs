using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;
using System.Data.Linq;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class FootballSubscriptionRepository : ISubscriptionRepository<FootballSubscription>
    {
        private readonly DataContext db;

        public FootballSubscriptionRepository()
        {
            db = new DataContext(ApplicationConfiguration.ConnectionString);
        }

        public bool Create(string userName, FootballSubscription model)
        {
            try
            {
                model.UserName = userName;

                db.GetTable<FootballSubscription>().InsertOnSubmit(model);
                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(string userName, int id)
        {
            try
            {
                var subToDelete = db.GetTable<FootballSubscription>().FirstOrDefault(s => s.Id == id && s.UserName == userName);

                db.GetTable<FootballSubscription>().DeleteOnSubmit(subToDelete);
                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public ICollection<FootballSubscription> GetAll(string userName)
        {
            var subList = db.GetTable<FootballSubscription>().Where(s => s.UserName == userName).ToList();

            return subList;
        }

        public FootballSubscription GetById(string userName, int id)
        {
            var sub = db.GetTable<FootballSubscription>().FirstOrDefault(s => s.Is == id && s.UserName == userName);

            return sub;
        }

        public bool Update(string userName, FootballSubscription model)
        {
            try
            {
                var subToUpdate = db.GetTable<FootballSubscription>().FirstOrDefault(s => s.Id == model.Id && s.UserName == userName);

                subToUpdate.Name = model.Name;
                subToUpdate.Description = model.Description;
                subToUpdate.LastRunTime = model.LastRunTime;
                subToUpdate.CronParams = model.CronParams;

                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
