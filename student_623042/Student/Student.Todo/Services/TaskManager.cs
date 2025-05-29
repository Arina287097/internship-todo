using System.Text.Json;
using System.Threading.Tasks;
using Student.Todo.Models;

namespace Student.Todo.Services
{
    /// <summary>
    /// Управлять Задачами
    /// </summary>
    public class TaskManager
    {
        private List<TodoItem> tasks = new List<TodoItem>();
        private const string SaveFilePath = "tasks.json";

        public TaskManager() => LoadTasks();

        /// <summary>
        /// Создать новый экземпляр менеджера задач
        /// </summary>

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void AddTask(TodoItem task)
        {
            tasks.Add(task);
            SaveTasks();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="task">Задача</param>
        public void RemoveTask(TodoItem task)
        {
            tasks.Remove(task);
            SaveTasks();
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Список задач</returns>
        public List<TodoItem> GetTasks() => tasks;

        /// <summary>
        /// Проверка существования файла tasks.json
        /// </summary>
        private void LoadTasks()
        {
            if (File.Exists(SaveFilePath))
            {
                string json = File.ReadAllText(SaveFilePath);
                tasks = JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
            }
        }

        /// <summary>
        /// Преобразовать список задач в JSON-строку
        /// </summary>
        private void SaveTasks()
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(SaveFilePath, json);
        }
    }
}