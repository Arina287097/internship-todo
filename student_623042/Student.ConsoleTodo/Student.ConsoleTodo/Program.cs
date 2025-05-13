namespace Student.ConsoleTodo
{
    /// <summary>
    /// 
    /// </summary> 
    class Program
    {
        /// <summary>
        /// список задач
        /// </summary>
        static List<Task> tasks = new List<Task>();

        static void Main(string[] args)
        {
            /// <summary>
            /// основоной цикл программы
            /// </summary>
            while (true)
            {
                ShowMenu();
                int choice = GetMenuChoice();
                /// <summary>
                /// выбор пользователя
                /// </summary>
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
        /// <summary>
        /// метод отображения меню
        /// </summary>
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Для вызова выполняемой подпрограммы укажите ее номер и нажмите Enter:");
            Console.WriteLine("1 - Посмотреть список задач");
            Console.WriteLine("2 - Добавить задачу");
        }
        /// <summary>
        /// метод для получения выбора пользователя
        /// </summary>
        /// <returns>целое число-выбранный пункт меню</returns>
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
        /// <summary>
        /// метод для добавления задач
        /// </summary>
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
        /// <summary>
        /// метод для просмотра задач
        /// </summary>
        static void ShowTasks()
        {
            
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
            while (true)
            {
                Console.WriteLine("Для выхода в главное меню нажмите Esc");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }
    }
    /// </summary>
    /// Класс для представления задач
    /// </summary>
    class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}