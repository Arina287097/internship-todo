using ConsoleTodo.Managers;
using ConsoleTodo.Services;

var taskService = new TaskService();
var menuManager = new MenuManager(taskService);

while (true)
{
    menuManager.ShowMainMenu();

    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        menuManager.ProcessInput(choice);
    }
    else
    {
        Console.WriteLine("Ошибка ввода. Введите число.");
    }
}