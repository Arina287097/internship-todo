using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// Класс для представления задач
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Свойство для заголовка задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Свойство для описания задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Метод для получения списка задач
        /// </summary>
        /// <returns>Строковое значение задачи</returns>
        public override string ToString()
        {
            return $"{Title}-{Description}";
        }
    }
}