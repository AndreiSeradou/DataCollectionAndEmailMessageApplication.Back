using Microsoft.Data.Sqlite;
using OmegaSoftware.TestProject.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models.DTOs;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class GoogleTranslateSubscriptionRepository : ISubscriptionRepository<GoogleTranslateSubscription>
    {
        public bool Create(string userName, GoogleTranslateSubscription model)
        {
            var sqlExpression = $"INSERT INTO GoogleSubscription (name,cronParams,description,lastruntime,userName) VALUES ('{model.Name}','{model.CronParams}','{model.Description}',{model.LastRunTime},'{userName}')";
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

        public bool Delete(string userName, int id)
        {
            string sqlExpression = $"DELETE  FROM GoogleSubscription WHERE id='{id}' And userName = '{userName}'";
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

        public ICollection<GoogleTranslateSubscription> GetAll(string userName)
        {
            List<GoogleTranslateSubscription> subList = new List<GoogleTranslateSubscription>();
            string sqlExpression = $"SELECT * FROM GoogleSubscription WHERE userName = {userName}";

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
                            subList.Add(new GoogleTranslateSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), LastRunTime = reader.GetDateTime(4), UserName = reader.GetString(5) }); 
                        }
                    }
                }
            }

            return subList;
        }

        public GoogleTranslateSubscription GetById(string userName, int id)
        {
            GoogleTranslateSubscription googleSubscription = default;
            var sqlExpression = $"SELECT * FROM GoogleSubscription WHERE id = {id} AND userName = {userName}";

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        googleSubscription = new GoogleTranslateSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), LastRunTime = reader.GetDateTime(4), UserName = reader.GetString(5) };
                    }
                }
            }

            return googleSubscription;
        }

        public bool Update(string userName, GoogleTranslateSubscription model)
        {
            string sqlExpression = $"UPDATE GoogleSubscription SET name = {model.Name}, cronParams = {model.CronParams}, description = {model.Description}, userName = {model.UserName}, lastruntime = {model.LastRunTime} WHERE Name='{userName}'";
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
