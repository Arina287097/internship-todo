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

        /// <summary>
        /// Конструктор с параметрами name и description
        /// </summary>
        /// <param name="name">Название задачи</param>
        /// <param name="description">Описание задачи</param>
        // для инциализации объекта 
        public Task () { }

        // Конструктор с параметрами name и description
        public Task(string name, string description)
        {
            Title = name;
            Description = description;
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Строковое значение задачи</returns>
        public override string ToString()
        {
            return $"{Title}-{Description}";
        }
    }
}