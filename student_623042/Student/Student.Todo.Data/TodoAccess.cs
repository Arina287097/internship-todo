

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


using Student.Todo.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Student.Todo.Data
{
    public class TodoAccess
    public class TodoAccess : ITodoRepository
    {
        private readonly string _connectionString;

        public TodoAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Сохранить задачу (добавление или обновление)
        /// </summary>
        /// <param name="task">Задача</param>
        public void SaveTask(TodoTask task)
        /// <inheritdoc />
        public List<TodoTask> GetAllTasks()
        {
            var tasks = new List<TodoTask>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT Id, Title, Description FROM Tasks", connection))
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

        /// <summary>
        /// Получить все задачи
        /// </summary>
        /// <returns>Задачи</returns>
        public List<TodoTask> GetTasks()
        /// <inheritdoc />
        public TodoTask GetTaskById(int id)
        {
            var tasks = new List<TodoTask>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT Id, Title, Description FROM Tasks WHERE Id = @Id", connection))
            {
                var command = new SqlCommand("SELECT Id, Title, Description FROM Tasks", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    if (reader.Read())
                    {
                        tasks.Add(new TodoTask(
                        return new TodoTask(
                            reader["Title"].ToString(),
                            reader["Description"].ToString())
                        {
                            Id = Convert.ToInt32(reader["Id"])
                        });
                        };
                    }
                }
            }
            return tasks;
            return null;
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id">ИД задачи</param>
        /// <inheritdoc />
        public void SaveTask(TodoTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = task.Id == 0 ?
                    "INSERT INTO Tasks (Title, Description) VALUES (@Title, @Description); SELECT SCOPE_IDENTITY();" :
                    "UPDATE Tasks SET Title = @Title, Description = @Description WHERE Id = @Id";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);

                    if (task.Id != 0)
                    {
                        command.Parameters.AddWithValue("@Id", task.Id);
                    }

                    connection.Open();

                    if (task.Id == 0)
                    {
                        // Для новой задачи получаем сгенерированный ID
                        var newId = Convert.ToInt32(command.ExecuteScalar());
                        task.Id = newId;
                    }
                    else
                    {
                        // Для существующей задачи проверяем, что она была обновлена
                        if (command.ExecuteNonQuery() == 0)
                            throw new DbUpdateConcurrencyException($"Задача с ID {task.Id} не найдена");
                    }
                }
            }
        }

        /// <inheritdoc />
        public void DeleteTask(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("DELETE FROM Tasks WHERE Id = @Id", connection))
            {
                var command = new SqlCommand("DELETE FROM Tasks WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                connection.Open();
                command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() == 0)
                    throw new DbUpdateConcurrencyException($"Задача с ID {id} не найдена");
            }
        }
    }
}