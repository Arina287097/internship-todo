using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// Класс для управления списком задач
    /// </summary>
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        /// <summary>
        /// Метод для добавления задачи
        /// </summary>
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }

        /// <summary>
        /// Метод для удаления задачи
        /// </summary>
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
        }

        /// <summary>
        /// Метод для получения списка задач
        /// </summary>
        /// <returns>Список задач</returns>
        public List<Task> GetTasks()
        {
            return tasks;
        }
    }
}