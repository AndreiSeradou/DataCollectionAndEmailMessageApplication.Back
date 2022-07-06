using System.Data.Linq;
using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext db;
        private readonly ConnectionStrings _connectionStrings;

        public UserRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            db = new DataContext(_connectionStrings.SqLiteConnectionString);
        }

        public bool Create(User model)
        {
            try
            {
                db.GetTable<User>().InsertOnSubmit(model);
                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var userToDelete = db.GetTable<User>().FirstOrDefault(u => u.Id == id);

                db.GetTable<User>().DeleteOnSubmit(userToDelete);
                db.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public ICollection<User> GetAll()
        {
            var userList = db.GetTable<User>().ToList();

            return userList;
        }

        public User GetByEmail(string userEmail)
        {
            var user = db.GetTable<User>().FirstOrDefault(u => u.Email == userEmail);

            return user;
        }

        public User GetByName(string userName)
        {
            var user = db.GetTable<User>().FirstOrDefault(u => u.UserName == userName);

            return user;
        }

        public bool Update(User model)
        {
            try
            {
                var userToUpdate = db.GetTable<User>().FirstOrDefault(u => u.Id == model.Id);

                userToUpdate.Name = model.Name;
                userToUpdate.Role = model.Role;
                userToUpdate.Password = model.Password;
                userToUpdate.Email = model.Email;

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
