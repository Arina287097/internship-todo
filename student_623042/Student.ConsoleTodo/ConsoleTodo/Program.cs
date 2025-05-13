namespace Student.ConsoleTodo
{
    /// <summary>
    /// Основоной класс приложения, роеднозначенный для управления задачами
    /// </summary>
    /// <remarks>
    /// Этот класс содержит основной цикл программы.
    /// Он позволяет добавлять задач и просматривать их.
    /// </remarks>
    class Program
    {
        /// Список задач, хранящий все добавленные задачи
        static List<Task> tasks = new List<Task>();
        /// <summary>
        /// Основоной цикл программы, который запускает меню и обрабатывает выбор пользователя
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
                int choice = GetMenuChoice();
                // Обработка выбора пользователя
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
        /// Метод отображения меню, с доступными опциями
        /// </summary>
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Для вызова выполняемой подпрограммы укажите ее номер и нажмите Enter:");
            Console.WriteLine("1 - Посмотреть список задач");
            Console.WriteLine("2 - Добавить задачу");
        }
        /// <summary>
        /// Метод для получения выбора пользователя
        /// </summary>
        /// <returns>Целое число-выбранный пункт меню</returns>
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
        /// Метод для добавления новых задач в список задач
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
        /// Метод для отображения задач в списке
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
    /// Класс для представления задач с заголовком и описанием 
    /// </summary>
    class Task
    {
        /// </summary>
        /// Заголовок задачи
        /// </summary>
        public string Title { get; set; }
        /// </summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }
    }
}
