using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1
{
    /// <summary>
    /// Диспетчер задач
    /// </summary>
    public class TaskManager
    {
        private List<TodoTask> tasks = new List<TodoTask>();

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void AddTask(TodoTask task)
        {
            tasks.Add(task);
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void RemoveTask(TodoTask task)
        {
            tasks.Remove(task);
        }

        public TodoTask GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }


        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Список задач</returns>
        public List<TodoTask> GetTasks() => tasks;
    }
}
