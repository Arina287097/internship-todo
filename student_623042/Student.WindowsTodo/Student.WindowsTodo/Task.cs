using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student.WindowsTodo
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Task
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
        public Task() { }

        // Конструктор с параметрами name и description
        public Task(string name, string description)
        {
            Title = name;
            Description = description;
        }
        public override string ToString()
        {
            return $"{Title} - {Description}";
        }
    }
}