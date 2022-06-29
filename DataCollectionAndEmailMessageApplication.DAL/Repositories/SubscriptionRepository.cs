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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public ICollection<Subscription> GetAll()
        {
            List<Subscription> subList = new List<Subscription>();
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
                            subList.Add(new Subscription { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CronParams = reader.GetString(3), UserId = reader.GetInt32(4), ApiId = reader.GetInt32(5) });
                        }
                    }
                }
            }

            return subList;
        }

        public void Create()
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

        public void Update()
        {
            string sqlExpression = "UPDATE Users SET Age=20 WHERE Name='Tom'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();

                Console.WriteLine($"Обновлено объектов: {number}");
            }
        }

        public void Delete()
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
