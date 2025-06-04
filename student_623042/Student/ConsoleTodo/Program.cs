using Student.ConsoleTodo;
using Student.Todo.Services;

var TaskManager = new TaskManager();
var menuManager = new MenuManager(TaskManager);

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
