using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.WindowsTodo
{
    /// <summary>
    /// Управлять Задачами
    /// </summary>
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Список задач</returns>
        public List<Task> GetTasks()
        {
            return tasks;
        }
    }
}