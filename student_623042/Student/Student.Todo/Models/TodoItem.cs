namespace Student.Todo.Models
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Заголовок задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описани задачи
        /// </summary>
        public string Description { get; set; }

        // для инциализации объекта 
        public TodoItem() { }

        // Конструктор с параметрами name и description
        public TodoItem(string title, string description)
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