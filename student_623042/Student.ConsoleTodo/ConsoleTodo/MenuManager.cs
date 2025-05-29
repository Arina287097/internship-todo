using Student.Todo.Models;
using Student.Todo.Services;

namespace ConsoleTodo.Managers;

/// <summary>
/// Управление пользовательским интерфейсом 
/// </summary>
public class MenuManager
{
    private readonly TaskManager _taskManager;

    public MenuManager(TaskManager taskManager)
    {
        _taskManager = taskManager;
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
        Console.WriteLine("3 - Удалить задачу");
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
            case 3:
                RemoveTask();
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
        var tasks = _taskManager.GetTasks();

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
        var task = new TodoItem
        {
            Title = GetInput("Введите заголовок задачи:"),
            Description = GetInput("Введите описание задачи:")
        };

        _taskManager.AddTask(task);
        Console.WriteLine("Задача добавлена!");
        WaitForContinue();
    }

    /// <summary>
    /// Удалиить задачу
    /// </summary>
    private void RemoveTask()
    {
        var tasks = _taskManager.GetTasks();

        if (tasks.Count == 0)
        {
            Console.WriteLine("Задач для удаления нет.");
            WaitForContinue();
            return;
        }

        Console.Write("Введите номмер задачи для удаления:");
        if (int.TryParse(Console.ReadLine(), out int taskNumber) &&
            taskNumber > 0 && taskNumber <= tasks.Count)
        {
            _taskManager.RemoveTask(tasks[taskNumber - 1]);
            Console.WriteLine("Задача удалена");
        }
        else
        {
            Console.WriteLine("Неверный номер задачи");
        }
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
