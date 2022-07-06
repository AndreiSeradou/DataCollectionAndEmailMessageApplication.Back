using System.Data.Linq;
using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class WheatherSubscriptionRepository : ISubscriptionRepository<WheatherSubscription>
    {
        private readonly DataContext db;
        private readonly ConnectionStrings _connectionStrings;

        public WheatherSubscriptionRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            db = new DataContext(_connectionStrings.SqLiteConnectionString);
        }

        public ICollection<WheatherSubscription> GetAll(string userName)
        {
            var subList = db.GetTable<WheatherSubscription>().Where(s => s.UserName == userName).ToList();

            return subList;
        }

        public WheatherSubscription GetById(string userName, int id)
        {
            var sub = db.GetTable<WheatherSubscription>().FirstOrDefault(s => s.Is == id && s.UserName == userName);

            return sub;
        }

        public bool Create(string userName, WheatherSubscription model)
        {
            try
            {
                model.UserName = userName;

                db.GetTable<WheatherSubscription>().InsertOnSubmit(model);
                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Update(string userName, WheatherSubscription model)
        {
            try
            {
                var subToUpdate = db.GetTable<WheatherSubscription>().FirstOrDefault(s => s.Id == model.Id && s.UserName == userName);

                subToUpdate.Name = model.Name;
                subToUpdate.Description = model.Description;
                subToUpdate.LastRunTime = model.LastRunTime;
                subToUpdate.CronParams = model.CronParams;
                subToUpdate.City = model.City;
                subToUpdate.Date = model.Date;

                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(string userName ,int id)
        {
            try
            {
                var subToDelete = db.GetTable<WheatherSubscription>().FirstOrDefault(s => s.Id == id && s.UserName == userName);

                db.GetTable<WheatherSubscription>().DeleteOnSubmit(subToDelete);
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
