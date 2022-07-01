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
    public class UserRepository : IUserRepository
    {
        public void Create(User model)
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

        public ICollection<User> GetAll()
        {
            List<User> userList = new List<User>();
            string sqlExpression = "SELECT * FROM Subscription WHERE UserId = userId";
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
                            userList.Add(new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2) });
                        }
                    }
                }
            }

            return userList;
        }

        public User GetById(int userId)
        {
            User user = default;

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
                command.Parameters.AddWithValue("$id", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User { Id = reader.GetInt32(0), Name = reader.GetString(1), Email = reader.GetString(2) };
                    }
                }
            }

            return user;
        }

        public void Update(User model)
        {
            string sqlExpression = "UPDATE Users SET Age=20 WHERE Name='Tom'";
            using (var connection = new SqliteConnection("Data Source=usersdata.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
            }
        }
    }
}
