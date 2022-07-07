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
    }
}
