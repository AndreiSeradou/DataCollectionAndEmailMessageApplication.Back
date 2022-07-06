using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;
using System.Data.Linq;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class GoogleTranslateSubscriptionRepository : ISubscriptionRepository<GoogleTranslateSubscription>
    {
        private readonly DataContext db;
        private readonly ConnectionStrings _connectionStrings;

        public GoogleTranslateSubscriptionRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            db = new DataContext(_connectionStrings.SqLiteConnectionString);
        }

        public bool Create(string userName, GoogleTranslateSubscription model)
        {
            try
            {
                model.UserName = userName;

                db.GetTable<GoogleTranslateSubscription>().InsertOnSubmit(model);
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
                var subToDelete = db.GetTable<GoogleTranslateSubscription>().FirstOrDefault(s => s.Id == id && s.UserName == userName);

                db.GetTable<GoogleTranslateSubscription>().DeleteOnSubmit(subToDelete);
                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public ICollection<GoogleTranslateSubscription> GetAll(string userName)
        {
            var subList = db.GetTable<GoogleTranslateSubscription>().Where(s => s.UserName == userName).ToList();

            return subList;
        }

        public GoogleTranslateSubscription GetById(string userName, int id)
        {
            var sub = db.GetTable<GoogleTranslateSubscription>().FirstOrDefault(s => s.Is == id && s.UserName == userName);

            return sub;
        }

        public bool Update(string userName, GoogleTranslateSubscription model)
        {
            try
            {
                var subToUpdate = db.GetTable<GoogleTranslateSubscription>().FirstOrDefault(s => s.Id == model.Id && s.UserName == userName);

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
