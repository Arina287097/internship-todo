using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Student.Todo.Models;
using Student.Todo.Services;

public class IndexModel : PageModel
{
    private readonly TaskManager _taskManager;

    /// <summary>
    /// Конструктор класса IndexModel
    /// </summary>
    /// <param name="taskManager">Менеджер задач</param>
    public IndexModel(TaskManager taskManager)
    {
        _taskManager = taskManager;
    }

    /// <summary>
    /// Заголовок новой задачи
    /// </summary>
    [BindProperty]
    public string NewTaskTitle { get; set; }

    /// <summary>
    /// Описание новой задачи
    /// </summary>
    [BindProperty]
    public string NewTaskDescription { get; set; }

    /// <summary>
    /// Отобразить список на странице
    /// </summary>
    public List<TodoTask> Tasks { get; set; }

    /// <summary>
    /// Загрузить список задач в taskManager
    /// </summary>
    public void OnGet()
    {
        Tasks = _taskManager.GetTasks();
    }

    /// <summary>
    /// Добавить задачу
    /// </summary>
    /// <returns>Текущую страницу</returns>
    public IActionResult OnPostAdd()
    {
        if (!string.IsNullOrWhiteSpace(NewTaskTitle))
        {
            var task = new TodoTask(NewTaskTitle, NewTaskDescription);
            _taskManager.AddTask(task);
        }
        return RedirectToPage();
    }

    /// <summary>
    /// Удалить задачу
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <returns>Текущую страницу</returns>
    public IActionResult OnPostDelete(string title, string description)
    {
        var taskToRemove = _taskManager.GetTasks()
            .FirstOrDefault(t => t.Title == title && t.Description == description);

        if (taskToRemove != null)
        {
            _taskManager.RemoveTask(taskToRemove);
        }
        return RedirectToPage();
    }
}