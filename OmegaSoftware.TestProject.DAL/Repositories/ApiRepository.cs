using Microsoft.Data.Sqlite;
using OmegaSoftware.TestProject.DAL.Configuration;
using OmegaSoftware.TestProject.DAL.Interfaces.Repositories;
using OmegaSoftware.TestProject.DAL.Models;
using Microsoft.Extensions.Options;

namespace OmegaSoftware.TestProject.DAL.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private readonly ConnectionStrings _connectionStrings;

        public ApiRepository(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            _connectionStrings = optionsMonitor.CurrentValue;
        }

        public bool Create(Api model)
        {
            var sqlExpression = $"INSERT INTO Api (name,url,apiKey,apiHost,apiKeyHeader,apiHostHeader) VALUES ('{model.Name}','{model.Url}','{model.ApiKey}','{model.ApiHost}','{model.ApiKeyHeader}','{model.ApiHostHeader}')";
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
            string sqlExpression = $"DELETE  FROM Api WHERE id={id}";
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

        public ICollection<Api> GetAll()
        {
            List<Api> apiList = new List<Api>();
            string sqlExpression = $"SELECT * FROM Api";

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
                            apiList.Add(new Api { Id = reader.GetInt32(0), Name = reader.GetString(1), Url = reader.GetString(2),  ApiKey = reader.GetString(3),  ApiHost = reader.GetString(4), ApiKeyHeader = reader.GetString(5), ApiHostHeader = reader.GetString(6) });
                        }
                    }
                }
            }

            return apiList;
        }

        public Api GetById(int id)
        {
            Api api = default;
            var sqlExpression = $"SELECT * FROM Api WHERE id = {id}";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        api = new Api { Id = reader.GetInt32(0), Name = reader.GetString(1), Url = reader.GetString(2), ApiKey = reader.GetString(3), ApiKeyHeader = reader.GetString(4), ApiHost = reader.GetString(5), ApiHostHeader = reader.GetString(6) };
                    }
                }
            }

            return api;
        }

        public Api GetByName(string apiName)
        {
            Api api = default;
            var sqlExpression = $"SELECT * FROM Api WHERE apiName = {apiName}";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        api = new Api { Id = reader.GetInt32(0), Name = reader.GetString(1), Url = reader.GetString(2), ApiKey = reader.GetString(3), ApiKeyHeader = reader.GetString(4), ApiHost = reader.GetString(5), ApiHostHeader = reader.GetString(6) };
                    }
                }
            }

            return api;
        }

        public bool Update(Api model)
        {
            string sqlExpression = $"UPDATE Api SET name = {model.Name}, url = {model.Url}, apiKey = {model.ApiKey}, apiHost = {model.ApiHost}, apiKeyHeader = {model.ApiKeyHeader}, apiHostHeader = {model.ApiHostHeader}  WHERE Name='{model.Name}'";
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
