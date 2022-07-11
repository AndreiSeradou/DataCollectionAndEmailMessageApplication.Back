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
            var sqlExpression = "INSERT INTO Users (name,email,password,role,numberOfUsesApis,numberOfRunningJobs) VALUES (@Name, @Email, @Password, @Role, @NumberOfUsesApis, @NumberOfRunningJobs)";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter emailParam = new SqliteParameter("@Email", model.Email);
                SqliteParameter passwordParam = new SqliteParameter("@Password", model.Password);
                SqliteParameter roleParam = new SqliteParameter("@Role", model.Role);
                SqliteParameter numberOfUsesApis = new SqliteParameter("@NumberOfUsesApis", model.NumberOfUsesApis);
                SqliteParameter numberOfRunningJobs = new SqliteParameter("@NumberOfRunningJobs", model.NumberOfRunningJobs);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(passwordParam);
                command.Parameters.Add(roleParam);
                command.Parameters.Add(numberOfUsesApis);
                command.Parameters.Add(numberOfRunningJobs);

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
            string sqlExpression = $"DELETE  FROM Users WHERE id = @Id";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter idParam = new SqliteParameter("@Id", id);

                command.Parameters.Add(idParam);

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
            string sqlExpression = "SELECT * FROM Users";

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
                            userList.Add(new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Password = reader.GetString(3), Role = reader.GetString(4), NumberOfUsesApis = reader.GetInt32(5), NumberOfRunningJobs = reader.GetInt32(6) });
                        }
                    }
                }
            }

            return userList;
        }

        public User GetByEmail(string userEmail)
        {
            User user = default;
            var sqlExpression = $"SELECT * FROM Users WHERE email = @Email";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter emailParam = new SqliteParameter("@Email", userEmail);

                command.Parameters.Add(emailParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Password = reader.GetString(3), Role = reader.GetString(4), NumberOfUsesApis = reader.GetInt32(5), NumberOfRunningJobs = reader.GetInt32(6) };
                    }
                }
            }

            return user;
        }

        public User GetByName(string userName)
        {
            User user = default;
            var sqlExpression = $"SELECT * FROM Users WHERE name = @Name";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", userName);

                command.Parameters.Add(nameParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2), Password = reader.GetString(3), Role = reader.GetString(4), NumberOfUsesApis = reader.GetInt32(5), NumberOfRunningJobs = reader.GetInt32(6) };
                    }
                }
            }

            return user;
        }

        public bool Update(User model)
        {
            string sqlExpression = $"UPDATE Users SET name = @Name, email = @Email, role = @Role, numberOfUsesApis = @NumberOfUsesApis, numberOfRunningJobs = @NumberOfRunningJobs  WHERE name = @Name";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter emailParam = new SqliteParameter("@Email", model.Email);
                SqliteParameter passwordParam = new SqliteParameter("@Password", model.Password);
                SqliteParameter roleParam = new SqliteParameter("@Role", model.Role);
                SqliteParameter numberOfUsesApis = new SqliteParameter("@NumberOfUsesApis", model.NumberOfUsesApis);
                SqliteParameter numberOfRunningJobs = new SqliteParameter("@NumberOfRunningJobs", model.NumberOfRunningJobs);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(passwordParam);
                command.Parameters.Add(roleParam);
                command.Parameters.Add(numberOfUsesApis);
                command.Parameters.Add(numberOfRunningJobs);

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
