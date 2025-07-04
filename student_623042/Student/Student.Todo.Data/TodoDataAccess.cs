// Student.Todo.Data/TodoDataAccess.cs
using Student.Todo.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Student.Todo.Data
{
    public class TodoDataAccess
    {
        private readonly string _connectionString;

        public TodoDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Сохранить задачу (добавление или обновление)
        public void SaveTask(TodoTask task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "IF EXISTS (SELECT 1 FROM Tasks WHERE Id = @Id) " +
                    "UPDATE Tasks SET Title = @Title, Description = @Description WHERE Id = @Id " +
                    "ELSE INSERT INTO Tasks (Title, Description) VALUES (@Title, @Description)",
                    connection);

                command.Parameters.AddWithValue("@Id", task.Id);
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Получить все задачи
        public List<TodoTask> GetTasks()
        {
            var tasks = new List<TodoTask>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT Id, Title, Description FROM Tasks", connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new TodoTask(
                            reader["Title"].ToString(),
                            reader["Description"].ToString())
                        {
                            Id = Convert.ToInt32(reader["Id"])
                        });
                    }
                }
            }

            return tasks;
        }

        // Удалить задачу
        public void DeleteTask(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Tasks WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}