using Microsoft.EntityFrameworkCore;
using Student.Todo.Models;
using System.Collections.Generic;
using System.Linq;

namespace Student.Todo.Data
{
    public class EfTodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public EfTodoRepository(TodoContext context)
        {
            _context = context;
        }

        public List<TodoTask> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public void AddTask(TodoTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TodoTask task)
        {
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public TodoTask GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

    }
}