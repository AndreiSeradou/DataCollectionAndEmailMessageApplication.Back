using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using OmegaSoftware.TestProject.DAL.Configuration;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public UserRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            _connectionStrings = optionsMonitor.CurrentValue;
        }

        public bool Create(User model)
        {
            var sqlExpression = $"INSERT INTO User (name,email,password,role,numberOfUsesApis,numberOfRinningJobs) VALUES ('{model.Name}','{model.Email}','{model.Password}','{model.Role}','{model.NumberOfUsesApis},'{model.NumberOfRunningJobs}')";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            string sqlExpression = $"DELETE  FROM User WHERE id={id}";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public ICollection<User> GetAll()
        {
            List<User> userList = new List<User>();
            string sqlExpression = "SELECT * FROM User";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userList.Add(new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Role = reader.GetString(4), NumberOfUsesApis = reader.GetInt32(5), NumberOfRunningJobs = reader.GetInt32(6) });
                        }
                    }
                }
            }

            return userList;
        }

        public User GetByEmail(string userEmail)
        {
            User user = default;
            var sqlExpression = $"SELECT * FROM User WHERE email = {userEmail}";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Role = reader.GetString(4), NumberOfUsesApis = reader.GetInt32(5), NumberOfRunningJobs = reader.GetInt32(6) };
                    }
                }
            }

            return user;
        }

        public User GetByName(string userName)
        {
            User user = default;
            var sqlExpression = $"SELECT * FROM User WHERE name = {userName}";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Role = reader.GetString(4), NumberOfUsesApis = reader.GetInt32(5), NumberOfRunningJobs = reader.GetInt32(6) };
                    }
                }
            }

            return user;
        }

        public bool Update(User model)
        {
            string sqlExpression = $"UPDATE User SET name = {model.Name}, email = {model.Email}, role = {model.Role}, numberOfUsesApis = {model.NumberOfUsesApis}, numberOfRunningJobs = {model.NumberOfRunningJobs}  WHERE name='{model.Name}'";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }
    }
}
