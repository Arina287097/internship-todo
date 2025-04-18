namespace Student.ConsoleTodo
{
    class Program
    {
        static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
                int choice = GetMenuChoice();

                switch (choice)
                {
                    case 1:
                        ShowTasks();
                        break;
                    case 2:
                        AddTask();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод, попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Для вызова выполняемой подпрограммы укажите ее номер и нажмите Enter:");
            Console.WriteLine("1 - Посмотреть список задач");
            Console.WriteLine("2 - Добавить задачу");
            Console.WriteLine("Esc - Выйти");
        }

        static int GetMenuChoice()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        static void AddTask()
        {
            Console.Clear();
            Console.WriteLine("Введите заголовок задачи. По завершению ввода нажмите Enter:");
            string title = Console.ReadLine();

            Console.WriteLine("Введите описание задачи. По завершению ввода нажмите Enter:");
            string description = Console.ReadLine();

            tasks.Add(new Task { Title = title, Description = description });
            Console.WriteLine("Задача добавлена! Нажмите Enter для продолжения.");
            Console.ReadLine();
        }

        static void ShowTasks()
        {
            Console.Clear();
            Console.WriteLine("Список задач:");

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

            Console.WriteLine("Для возврата к списку подпрограмм нажмите Esc:");
            Console.ReadKey(true);
        }
    }

    class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}