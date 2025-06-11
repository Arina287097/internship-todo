using System;

namespace WebApplication1
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TodoTask
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Заголовок задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описани задачи
        /// </summary>
        public string Description { get; set; }

        // для инциализации объекта 
        public TodoTask() { }

        // Конструктор с параметрами name и description
        public TodoTask(string title, string description)
        {
            Title = title;
            Description = description;
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Строковое значение задачи</returns>
        public override string ToString()
        {
            return $"{Title} - {Description}";
        }
    }
}
