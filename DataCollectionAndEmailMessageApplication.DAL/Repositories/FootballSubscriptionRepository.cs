using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectionAndEmailMessageApplication.DAL.Repositories
{
    public class FootballSubscriptionRepository : IFootballSubscriptionRepository
    {
        public bool Create(string userName, FootballSubscription model)
        {
            var sqlExpression = $"INSERT INTO FootballSubscription (name,cronParams,description,userName) VALUES ('{model.Name}','{model.CronParams}','{model.Description}','{userName}')";
            int result;

            using (var connection = new SqliteConnection("Data Source=subscriptiondata.db"))
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
            string sqlExpression = $"DELETE  FROM FootballSubscription WHERE id='{id}' And userName = '{userName}'";
            int result;

            using (var connection = new SqliteConnection("Data Source=subscriptiondata.db"))
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

        public ICollection<FootballSubscription> GetAll(string userName)
        {
            List<FootballSubscription> subList = new List<FootballSubscription>();
            string sqlExpression = $"SELECT * FROM FootballSubscription WHERE userName = {userName}";

            using (var connection = new SqliteConnection("Data Source=subscriptiondata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            subList.Add(new FootballSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserName = reader.GetString(4) });
                        }
                    }
                }
            }

            return subList;
        }

        public FootballSubscription GetById(string userName, int id)
        {
            FootballSubscription footballSubscription = default;
            var sqlExpression = $"SELECT * FROM FootballSubscription WHERE id = {id} AND userName = {userName}";

            using (var connection = new SqliteConnection("Data Source=subscriptiondata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        footballSubscription = new FootballSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserName = reader.GetString(4) };
                    }
                }
            }

            return footballSubscription;
        }

        public bool Update(string userName, FootballSubscription model)
        {
            string sqlExpression = $"UPDATE FootballSubscription SET name = {model.Name}, cronParams = {model.CronParams}, description = {model.Description}, userName = {model.UserName}  WHERE Name='{userName}'";
            int result;

            using (var connection = new SqliteConnection("Data Source=subscriptiondata.db"))
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
