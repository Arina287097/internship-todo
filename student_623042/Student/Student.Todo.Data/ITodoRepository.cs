using Student.Todo.Models;
using System.Collections.Generic;

namespace Student.Todo.Data
{
    public interface ITodoRepository
    {
        List<TodoTask> GetAllTasks();
        TodoTask GetTaskById(int id);
        void AddTask(TodoTask task);
        void UpdateTask(TodoTask task);
        void DeleteTask(int id);
    }
}