using Student.Todo.Models;

namespace Student.Todo.Services
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

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Список задач</returns>
        public List<TodoTask> GetTasks() => tasks;
    }
}
