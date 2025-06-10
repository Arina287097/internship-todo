using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Student.Todo.Models;
using Student.Todo.Services;

public class IndexModel : PageModel
{
    private readonly TaskManager _taskManager;

    /// <summary>
    /// ����������� ������ IndexModel
    /// </summary>
    /// <param name="taskManager">�������� �����</param>
    public IndexModel(TaskManager taskManager)
    {
        _taskManager = taskManager;
    }

    /// <summary>
    /// ��������� ����� ������
    /// </summary>
    [BindProperty]
    public string NewTaskTitle { get; set; }

    /// <summary>
    /// �������� ����� ������
    /// </summary>
    [BindProperty]
    public string NewTaskDescription { get; set; }

    /// <summary>
    /// ���������� ������ �� ��������
    /// </summary>
    public List<TodoTask> Tasks { get; set; }

    /// <summary>
    /// ��������� ������ ����� � taskManager
    /// </summary>
    public void OnGet()
    {
        Tasks = _taskManager.GetTasks();
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <returns>������� ��������</returns>
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
    /// ������� ������
    /// </summary>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <returns>������� ��������</returns>
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