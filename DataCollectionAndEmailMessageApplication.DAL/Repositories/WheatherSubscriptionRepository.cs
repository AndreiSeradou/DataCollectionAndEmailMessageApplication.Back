using DataCollectionAndEmailMessageApplication.DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollectionAndEmailMessageApplication.DAL.Interfaces.Repositories;
//using System.Data.SQLite;

namespace DataCollectionAndEmailMessageApplication.DAL.Repositories
{
    public class WheatherSubscriptionRepository : IWheatherSubscriptionRepository
    {
        public ICollection<WheatherSubscription> GetAll()
        {
            List<WheatherSubscription> subList = new List<WheatherSubscription>();
            string sqlExpression = "SELECT * FROM Subscription";
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
                            subList.Add(new WheatherSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserId = reader.GetInt32(4), Param1 = reader.GetString(5), Param2 = reader.GetString(6) });
                        }
                    }
                }
            }

            return subList;
        }

        public WheatherSubscription GetById(int id)
        {
            WheatherSubscription wheatherSubscription = default;

            using (var connection = new SqliteConnection("Data Source=hello.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
        SELECT name
        FROM user
        WHERE id = $id
    ";
                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        wheatherSubscription = new WheatherSubscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserId = reader.GetInt32(4), Param1 = reader.GetString(5), Param2 = reader.GetString(6) };
                    }
                }
            }

            return wheatherSubscription;
        }

        public void Create(WheatherSubscription model)
        {

            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Users (Name, Age) VALUES ('Tom', 36)";
                int number = command.ExecuteNonQuery();
            }
        }

        public void Update(WheatherSubscription model)
        {
            string sqlExpression = "UPDATE Users SET Age=20 WHERE Name='Tom'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string sqlExpression = "DELETE  FROM Users WHERE Name='Tom'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();

                Console.WriteLine($"Удалено объектов: {number}");
            }
        }
    }
}
