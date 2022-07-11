using Microsoft.Data.Sqlite;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;
using Microsoft.Extensions.Options;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ConnectionStrings  _connectionStrings;

        public SubscriptionRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            _connectionStrings = optionsMonitor.CurrentValue;
        }

        public bool Create(string userName, Subscription model)
        {
            var sqlExpression = "INSERT INTO Subscription (name,cronExpression,description,apiParams,apiName,DateStart,lastRunTime,userName) VALUES (@Name,@CronExpression,@Description,@ApiParams,@ApiName,@DateStart,@LastRunTime,@UserName)";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter cronParam = new SqliteParameter("@CronExpression", model.CronExpression);
                SqliteParameter descParam = new SqliteParameter("@Description", model.Description);
                SqliteParameter apiParam = new SqliteParameter("@ApiParams", model.ApiParams);
                SqliteParameter apiNameParam = new SqliteParameter("@ApiName", model.ApiName);
                SqliteParameter dateParam = new SqliteParameter("@DateStart", model.DateStart);
                SqliteParameter lastRunParam = new SqliteParameter("@LastRunTime", model.LastRunTime);
                SqliteParameter userNameParam = new SqliteParameter("@UserName", userName);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(cronParam);
                command.Parameters.Add(descParam);
                command.Parameters.Add(apiParam);
                command.Parameters.Add(apiNameParam);
                command.Parameters.Add(dateParam);
                command.Parameters.Add(lastRunParam);
                command.Parameters.Add(userNameParam);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public bool Delete(string userName, int id)
        {
            string sqlExpression = "DELETE  FROM Subscription WHERE id = @Id AND userName = @UserName";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@UserName", userName);
                SqliteParameter idParam = new SqliteParameter("@Id", id);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(idParam);

                result = command.ExecuteNonQuery();
            }

            if (result < 1)
            {
                return false;
            }

            return true;
        }

        public ICollection<Subscription> GetAll(string userName)
        {
            List<Subscription> subList = new List<Subscription>();
            string sqlExpression = "SELECT * FROM Subscription WHERE userName = @UserName";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@UserName", userName);

                command.Parameters.Add(nameParam);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            subList.Add(new Subscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronExpression = reader.GetString(3), DateStart = reader.GetDateTime(4), ApiParams = reader.GetString(5), LastRunTime = reader.GetDateTime(6), ApiName = reader.GetString(7), UserName = reader.GetString(8) });
                        }
                    }
                }
            }

            return subList;
        }

        public ICollection<Subscription> GetAll()
        {
            List<Subscription> subList = new List<Subscription>();
            string sqlExpression = $"SELECT * FROM Subscription";

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
                            subList.Add(new Subscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronExpression = reader.GetString(3), DateStart = reader.GetDateTime(4), ApiParams = reader.GetString(5), LastRunTime = reader.GetDateTime(6), ApiName = reader.GetString(7), UserName = reader.GetString(8) });
                        }
                    }
                }
            }

            return subList;
        }

        public Subscription GetById(string userName, int id)
        {
            Subscription subscription = default;
            var sqlExpression = "SELECT * FROM Subscription WHERE id = @Id AND userName = @UserName";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@UserName", userName);
                SqliteParameter idParam = new SqliteParameter("@Id", id);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(idParam);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subscription = new Subscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronExpression = reader.GetString(3), DateStart = reader.GetDateTime(4), ApiParams = reader.GetString(5), LastRunTime = reader.GetDateTime(6), ApiName = reader.GetString(7), UserName = reader.GetString(8) };
                    }
                }
            }

            return subscription;
        }

        public bool Update(string userName, Subscription model)
        {
            string sqlExpression = $"UPDATE Subscription SET name = @Name, cronExpression = @CronExpression, dateStart = @DateStart, apiName = @ApiName, apiParams = @ApiParams, description = @Description, userName = @UserName, lastRunTime = @LastRunTime  WHERE Name = @UserName";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter cronParam = new SqliteParameter("@CronExpression", model.CronExpression);
                SqliteParameter descParam = new SqliteParameter("@Description", model.Description);
                SqliteParameter apiParam = new SqliteParameter("@ApiParams", model.ApiParams);
                SqliteParameter apiNameParam = new SqliteParameter("@ApiName", model.ApiName);
                SqliteParameter dateParam = new SqliteParameter("@DateStart", model.DateStart);
                SqliteParameter lastRunParam = new SqliteParameter("@LastRunTime", model.LastRunTime);
                SqliteParameter userNameParam = new SqliteParameter("@UserName", userName);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(cronParam);
                command.Parameters.Add(descParam);
                command.Parameters.Add(apiParam);
                command.Parameters.Add(apiNameParam);
                command.Parameters.Add(dateParam);
                command.Parameters.Add(lastRunParam);
                command.Parameters.Add(userNameParam);

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
