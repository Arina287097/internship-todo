
namespace ConsoleTodo.Services;

/// <summary>
/// Бизнес логика работы с задачами
/// </summary>
public class TaskService
{
    private readonly List<Task> _tasks = new();

    public IReadOnlyList<Task> GetAllTasks() => _tasks.AsReadOnly();

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }
}
