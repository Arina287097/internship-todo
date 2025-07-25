using Student.Todo.Models;
using System.Collections.Generic;

namespace Student.Todo.Data
{
    public interface ITodoRepository
    {
        /// <summary>
        /// Получить все задачи
        /// </summary>
        /// <returns></returns>
        List<TodoTask> GetAllTasks();

        /// <summary>
        /// Получить ИД задачи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TodoTask GetTaskById(int id);

        /// <summary>
        /// Добавить или Обновить задачу 
        /// </summary>
        /// <param name="task"></param>
        void SaveTask(TodoTask task);

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id"></param>
        void DeleteTask(int id);
    }
}