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
            var sqlExpression = "INSERT INTO Apis (name,url,apiKey,apiHost,apiKeyHeader,apiHostHeader) VALUES (@Name, @Url, @ApiKey, @ApiHost, @ApiKeyHeader, @ApiHostHeader)";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter urlParam = new SqliteParameter("@Url", model.Url);
                SqliteParameter keyParam = new SqliteParameter("@ApiKey", model.ApiKey);
                SqliteParameter hostParam = new SqliteParameter("@ApiHost", model.ApiHost);
                SqliteParameter keyHeader = new SqliteParameter("@ApiKeyHeader", model.ApiKeyHeader);
                SqliteParameter hostHeader = new SqliteParameter("@ApiHostHeader", model.ApiHostHeader);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(urlParam);
                command.Parameters.Add(keyParam);
                command.Parameters.Add(hostParam);
                command.Parameters.Add(keyHeader);
                command.Parameters.Add(hostHeader);

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
            string sqlExpression = "DELETE  FROM Apis WHERE id = @Id";
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

        public ICollection<Api> GetAll()
        {
            List<Api> apiList = new List<Api>();
            string sqlExpression = $"SELECT * FROM Apis";

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
            var sqlExpression = "SELECT * FROM Apis WHERE id = @Id";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter idParam = new SqliteParameter("@Id", id);

                command.Parameters.Add(idParam);

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
            var sqlExpression = "SELECT * FROM Apis WHERE name = @Name";

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", apiName);

                command.Parameters.Add(nameParam);

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
            string sqlExpression = "UPDATE Apis SET name = @Name, url = @Url, apiKey = @ApiKey, apiHost = @ApiHost, apiKeyHeader = @ApiKeyHeader, apiHostHeader = @ApiHostHeader  WHERE Name= @Name";
            int result;

            using (var connection = new SqliteConnection(_connectionStrings.SqLiteConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                SqliteParameter nameParam = new SqliteParameter("@Name", model.Name);
                SqliteParameter urlParam = new SqliteParameter("@Url", model.Url);
                SqliteParameter keyParam = new SqliteParameter("@ApiKey", model.ApiKey);
                SqliteParameter hostParam = new SqliteParameter("@ApiHost", model.ApiHost);
                SqliteParameter keyHeader = new SqliteParameter("@ApiKeyHeader", model.ApiKeyHeader);
                SqliteParameter hostHeader = new SqliteParameter("@ApiHostHeader", model.ApiHostHeader);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(urlParam);
                command.Parameters.Add(keyParam);
                command.Parameters.Add(hostParam);
                command.Parameters.Add(keyHeader);
                command.Parameters.Add(hostHeader);

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
