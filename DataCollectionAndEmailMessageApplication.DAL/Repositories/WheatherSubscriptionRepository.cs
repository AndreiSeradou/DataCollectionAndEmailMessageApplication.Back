using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
using Configuration;

namespace DataCollectionAndEmailMessageApplication.DAL.Repositories
{
    public class WheatherSubscriptionRepository : ISubscriptionRepository<WheatherSubscription>
    {
        public ICollection<WheatherSubscription> GetAll(string userName)
        {
            List<WheatherSubscription> subList = new List<WheatherSubscription>();
            string sqlExpression = $"SELECT * FROM WheatherSubscription WHERE userName = {userName}";

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
                            subList.Add(new WheatherSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserName = reader.GetString(7),  City = reader.GetString(4),  Date = reader.GetString(5), LastRunTime = reader.GetDateTime(6) });
                        }
                    }
                }
            }

            return subList;
        }

        public WheatherSubscription GetById(string userName, int id)
        {
            WheatherSubscription wheatherSubscription = default;
            var sqlExpression = $"SELECT * FROM WheatherSubscription WHERE id = {id} AND userName = {userName}";

            using (var connection = new SqliteConnection(ApplicationConfiguration.ConnectionString))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wheatherSubscription = new WheatherSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserName = reader.GetString(7), City = reader.GetString(4), Date = reader.GetString(5), LastRunTime = reader.GetDateTime(6) };
                    }
                }
            }

            return wheatherSubscription;
        }

        public bool Create(string userName, WheatherSubscription model)
        {
            var sqlExpression = $"INSERT INTO WheatherSubscription (name,cronParams,date,description,city,lastruntime,userName) VALUES ('{model.Name}','{model.CronParams}','{model.Date}','{model.Description}','{model.City}',{model.LastRunTime},'{userName}')";
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

        public bool Update(string userName, WheatherSubscription model)
        {
            string sqlExpression = $"UPDATE WheatherSubscription SET name = {model.Name}, cronParams = {model.CronParams}, date = {model.Date}, description = {model.Description}, city = {model.City}, userName = {model.UserName}, lastruntime = {model.LastRunTime}  WHERE Name='{userName}'";
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

        public bool Delete(string userName ,int id)
        {
            string sqlExpression = $"DELETE  FROM WheatherSubscription WHERE id='{id}' And userName = '{userName}'";
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
