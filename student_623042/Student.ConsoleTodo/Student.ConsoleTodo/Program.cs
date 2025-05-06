namespace Student.ConsoleTodo
{
    class Program
    {
        //список задач
        static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            //основоной цикл программы
            while (true)
            {
                ShowMenu();
                int choice = GetMenuChoice();
                //выбор пользователя
                switch (choice)
                {
                    case 1:
                        ShowTasks();
                        break;
                    case 2:
                        AddTask();
                        break; 
                }
            }
        }
        //метод отображения меню
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Для вызова выполняемой подпрограммы укажите ее номер и нажмите Enter:");
            Console.WriteLine("1 - Посмотреть список задач");
            Console.WriteLine("2 - Добавить задачу");
        }
        //метод для получения выбора пользователя
        static int GetMenuChoice()
        {
            while (true)
            {
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Ошибка");
                }
            }
        }
        //метод для добавления задач
        static void AddTask()
        {
            Console.WriteLine("Введите заголовок задачи. По завершению ввода нажмите Enter:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание задачи. По завершению ввода нажмите Enter:");
            string description = Console.ReadLine();

            tasks.Add(new Task { Title = title, Description = description });
            Console.WriteLine("Задача добавлена! Нажмите Enter для продолжения.");
            Console.ReadLine();
        }
        //метод для просмотра задач
        static void ShowTasks()
        {
            while (true)
            {
                Console.WriteLine("Список задач:");
                ConsoleKeyInfo keyInfo = Console.ReadKey();

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
                    }
                }

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Вы в меню");
                    break;
                }
            }
        }
    }
    //класс для представления задач
    class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}