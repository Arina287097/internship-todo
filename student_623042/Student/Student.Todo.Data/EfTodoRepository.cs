using Microsoft.EntityFrameworkCore;
using Student.Todo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student.Todo.Data
{
    public class EfTodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public EfTodoRepository(TodoContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public List<TodoTask> GetAllTasks()
        {
            return _context.Tasks.AsNoTracking().ToList();
        }

        /// <inheritdoc />
        public void SaveTask(TodoTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            // Если ID = 0, считаем что это новая задача
            if (task.Id == 0)
            {
                _context.Tasks.Add(task);
            }
            else
            {
                // Для существующей задачи
                var existingTask = _context.Tasks.Find(task.Id);
                if (existingTask == null)
                {
                    throw new DbUpdateConcurrencyException($"Задача с ID {task.Id} не найдена");
                }

                // Копируем все свойства из входной задачи в найденную
                _context.Entry(existingTask).CurrentValues.SetValues(task);
            }

            _context.SaveChanges();
        }

        /// <inheritdoc />
        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        /// <inheritdoc />
        public TodoTask GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

    }
}