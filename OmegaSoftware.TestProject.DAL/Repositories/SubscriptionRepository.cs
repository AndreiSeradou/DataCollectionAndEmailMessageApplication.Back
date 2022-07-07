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
            var sqlExpression = $"INSERT INTO Subscription (name,cronExpression,description,apiParams,apiName,DateStart,lastRunTime,userName) VALUES ('{model.Name}','{model.CronExpression}','{model.Description}','{model.ApiParams}','{model.ApiName}','{model.DateStart}','{model.LastRunTime}','{userName}')";
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

        public bool Delete(string userName, int id)
        {
            string sqlExpression = $"DELETE  FROM Subscription WHERE id='{id}' AND userName = '{userName}'";
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

        public ICollection<Subscription> GetAll(string userName)
        {
            List<Subscription> subList = new List<Subscription>();
            string sqlExpression = $"SELECT * FROM Subscription WHERE userName = {userName}";

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
            var sqlExpression = $"SELECT * FROM Subscription WHERE id = {id} AND userName = {userName}";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

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
            string sqlExpression = $"UPDATE Subscription SET name = {model.Name}, cronExpression = {model.CronExpression}, dateStart = {model.DateStart}, apiName = {model.ApiName} ,apiParams = {model.ApiParams}, description = {model.Description}, userName = {model.UserName}, lastRunTime = {model.LastRunTime}  WHERE Name='{userName}'";
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
