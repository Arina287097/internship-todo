using Microsoft.EntityFrameworkCore;
using Student.Todo.Models;

namespace Student.Todo.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoTask> Tasks { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTask>().ToTable("Tasks");
            modelBuilder.Entity<TodoTask>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoTask>().Property(t => t.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<TodoTask>().Property(t => t.Description).IsRequired();
        }
    }
}