









using Student.Todo.Models;
using System.Collections.Generic;

namespace Student.Todo.Data
{
    public interface ITodoRepository
    {
        /// <summary>
        /// Получить все задачи
        /// </summary>
        /// <returns>Список задач</returns>
        List<TodoTask> GetAllTasks();

        /// <summary>
        /// Получить ИД задачи
        /// </summary>
        /// <param name="id">ИД Задачи</param>
        /// <returns></returns>
        TodoTask GetTaskById(int id);

        /// <summary>
        /// Добавить или Обновить задачу 
        /// </summary>
        /// <param name="task">Задачу</param>
        void SaveTask(TodoTask task);

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id">ИД задачи</param>
        void DeleteTask(int id);
    }
}