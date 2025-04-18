class Program
{
    static void Main()
    {
        while (true)
        {
            ShowMenu();
            int choice = GetMenuChoice();

            switch (choice)
            {
                case 1:
                    ExecuteIfElse();
                    break;
                case 2:
                    ExecuteWhile();
                    break;
                case 3:
                    ExecuteDoWhile();
                    break;
                case 4:
                    ExecuteFor();
                    break;
                case 5:
                    ExecuteForeach();
                    break;
                case 6:
                    ExecuteSwitch();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Для вызова выполняемой подпрограммы укажите ее номер и нажмите Enter:");
        Console.WriteLine("1 - IF ELSE");
        Console.WriteLine("2 - WHILE");
        Console.WriteLine("3 - DO WHILE");
        Console.WriteLine("4 - FOR");
        Console.WriteLine("5 - FOREACH");
        Console.WriteLine("6 - SWITCH");
    }

    static int GetMenuChoice()
    {
        return Convert.ToInt32(Console.ReadLine());
    }
    // if else работает
    static void ExecuteIfElse()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите целое число для проверки (для возврата к списку подпрограмм нажмите Esc):");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) continue;

            if (int.TryParse(input, out int number))
            {
                if (number % 2 == 0)
                {
                    Console.WriteLine("Число четное.");
                }
                else
                {
                    Console.WriteLine("Число нечетное.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }

            if (!WaitForKey()) break; // Возврат в главное меню
        }
    }
    // While работает
    static void ExecuteWhile()
    {
        int count = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите число для запуска цикла (для возврата к списку подпрограмм нажмите Esc):");

            if (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.WriteLine("Введите корректное число.");
                continue;
            }

            int i = 0;
            while (i < count)
            {
                Console.WriteLine(i + 1);
                i++;
            }

            if (!WaitForKey()) break; // Возврат в главное меню
        }
    }
    //while do работает
    static void ExecuteDoWhile()
    {
        int count = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите количество повторений: ");
            int repetitions = Convert.ToInt32(Console.ReadLine());
            do
            {
                Console.WriteLine(count);
                count++;
            } while (count <= repetitions);

            if (!WaitForKey()) break; // Возврат в главное меню
        }
    }
    //for работает
    static void ExecuteFor()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите начальное число и количество повторений (для возврата к списку подпрограмм нажмите Esc):");

            Console.WriteLine("Начальное число:");
            int start = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Количество повторений:");
            int repeats = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Результат:");

            for (int i = start; i < start + repeats; i++)
            {
                Console.WriteLine(i);
            }

            if (!WaitForKey()) break; // Возврат в главное меню
        }
    }
    //foreach работает
    static void ExecuteForeach()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите несколько чисел, разделенных пробелом:");
            string[] input = Console.ReadLine().Split(' ');
            Console.WriteLine("Результат:");
            foreach (var item in input)
            {
                Console.WriteLine(item);
            }
            if (!WaitForKey()) break; // Возврат в главное меню
        }
    }
    //switch работает
    static void ExecuteSwitch()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите номер дня недели (1-7, для возврата к списку подпрограмм нажмите Esc):");

            if (int.TryParse(Console.ReadLine(), out int day))
            {
                string dayName;

                switch (day)
                {
                    case 1:
                        dayName = "Понедельник";
                        break;
                    case 2:
                        dayName = "Вторник";
                        break;
                    case 3:
                        dayName = "Среда";
                        break;
                    case 4:
                        dayName = "Четверг";
                        break;
                    case 5:
                        dayName = "Пятница";
                        break;
                    case 6:
                        dayName = "Суббота";
                        break;
                    case 7:
                        dayName = "Воскресенье";
                        break;
                    default:
                        dayName = "Некорректный ввод.";
                        break;
                }

                Console.WriteLine(dayName);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }

            if (!WaitForKey()) break; // Возврат в главное меню
        }
    }
    //esc enter работают
    static bool WaitForKey()
    {
        Console.WriteLine("Нажмите Enter, чтобы повторить выполнение подпрограммы, или Esc, чтобы вернуться в главное меню.");
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                return true; // Повторить выполнение подпрограммы
            }
            else if (key == ConsoleKey.Escape)
            {
                return false; // Возврат в главное меню
            }
        }
    }
}