using Microsoft.Data.Sqlite;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models.DTOs;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool Create(User model)
        {
            var sqlExpression = $"INSERT INTO User (name,email,password,role) VALUES ('{model.Name}','{model.Email}','{model.Password}','{model.Role}')";
            int result;

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
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
            string sqlExpression = $"DELETE  FROM User WHERE id='{id}'";
            int result;

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
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

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userList.Add(new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Role = reader.GetString(4) });
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

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Role = reader.GetString(4) };
                    }
                }
            }

            return user;
        }

        public User GetByName(string userName)
        {
            User user = default;
            var sqlExpression = $"SELECT * FROM User WHERE name = {userName}";

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Role = reader.GetString(4) };
                    }
                }
            }

            return user;
        }

        public bool Update(User model)
        {
            string sqlExpression = $"UPDATE User SET name = {model.Name}, email = {model.Email}, role = {model.Role}  WHERE name='{model.Name}'";
            int result;

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
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
