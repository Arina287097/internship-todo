using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace Student.WindowsTodo
{
    /// <summary>
    /// Управлять Задачами
    /// </summary>
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private const string SaveFilePath = "tasks.json";

        public TaskManager()
        {
            LoadTasks();
        }

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void AddTask(Task task)
        {
            tasks.Add(task);
            SaveTasks();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void RemoveTask(Task task)
        {
            tasks.Remove(task);
            SaveTasks();
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Список задач</returns>
        public List<Task> GetTasks()
        {
            return tasks;
        }

        private void LoadTasks()
        {
            if (File.Exists(SaveFilePath))
            {
                string json = File.ReadAllText(SaveFilePath);
                tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            }
        }

        private void SaveTasks()
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(SaveFilePath, json);
        }
    }
}