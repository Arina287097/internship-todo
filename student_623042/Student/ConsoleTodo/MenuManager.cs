using ConsoleTodo.Services;

namespace ConsoleTodo.Managers;

/// <summary>
/// Управление пользовательским интерфейсом 
/// </summary>
public class MenuManager
{
    private readonly TaskService _taskService;

    public MenuManager(TaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Отображение меню
    /// </summary>
    public void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1 - Посмотреть список задач");
        Console.WriteLine("2 - Добавить задачу");
    }

    /// <summary>
    /// Обработка выбора пользователя
    /// </summary>
    /// <param name="choice"></param>
    public void ProcessInput(int choice)
    {
        switch (choice)
        {
            case 1:
                ShowTasks();
                break;
            case 2:
                AddTask();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    /// <summary>
    /// Отображение задач в списке задач
    /// </summary>
    private void ShowTasks()
    {
        Console.WriteLine("Список задач:");
        var tasks = _taskService.GetAllTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Задач нет.");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Задача {i + 1}:");
                Console.WriteLine($"Заголовок: {tasks[i].Title}");
                Console.WriteLine($"Описание: {tasks[i].Description}");
                Console.WriteLine();
            }
        }

        WaitForEscape();
    }

    /// <summary>
    /// Добавить задачу
    /// </summary>
    private void AddTask()
    {
        var task = new Task
        {
            Title = GetInput("Введите заголовок задачи:"),
            Description = GetInput("Введите описание задачи:")
        };

        _taskService.AddTask(task);
        Console.WriteLine("Задача добавлена!");
        WaitForContinue();
    }

    /// <summary>
    /// Упрощение повторяещегося кода
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns>чтение строки</returns>
    private string GetInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    /// <summary>
    /// Выход в меню (для 1 выбора)
    /// </summary>
    private void WaitForEscape()
    {
        Console.WriteLine("Нажмите Esc для возврата в меню");
        while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
    }

    /// <summary>
    /// Выход в меню (для 2 выбора)
    /// </summary>
    private void WaitForContinue()
    {
        Console.WriteLine("Нажмите Enter для продолжения");
        Console.ReadLine();
    }
}
