using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            int choice = GetMenuChoice();

            switch (choice)
            {
                //if else
                case 1:
                    while (true)
                    {
                        Console.WriteLine("вы выбрали инструкцию If Else");
                        Console.WriteLine("нажмите Enter для запуска инструкции, для возврата к списку подпрограмм нажмите Esc");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                Console.WriteLine("Введите число для проверки:");
                                string input = Console.ReadLine();  

                                if (string.IsNullOrEmpty(input))
                                {
                                    Console.WriteLine("Вы ничего не ввели. Попробуйте снова.");
                                    continue;
                                }

                                if (int.TryParse(input, out int number))
                                {
                                    if (number % 2 == 0)
                                    {
                                        Console.WriteLine($"Число {number} четное.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Число {number} нечетное.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ошибка: введено не целое число.");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка");
                                continue;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Вы в меню");
                            break;
                        }
                       
                    }
                    break;
                //while
                case 2:
                    int count = 0;
                    while (true)
                    {
                        Console.WriteLine("вы выбрали инструкцию While");
                        Console.WriteLine("нажмите Enter для запуска инструкции, для возврата к списку подпрограмм нажмите Esc");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                Console.WriteLine("Введите число для проверки");
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
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка");
                                continue;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Вы в меню");
                            break;
                        }
                    }
                    break;
                //do while 
                case 3:
                    int repetitions = 0;
                    while (true)
                    {
                        Console.WriteLine("вы выбрали инструкцию While Do");
                        Console.WriteLine("нажмите Enter для запуска инструкции, для возврата к списку подпрограмм нажмите Esc");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                Console.WriteLine("Введите число для проверки");
                                if (!int.TryParse(Console.ReadLine(), out repetitions))
                                {
                                    Console.WriteLine("Введите корректное число.");
                                    continue;
                                }
                                int g = 0;
                                do
                                {
                                    Console.WriteLine(g + 1);
                                    g++;
                                } while (g < repetitions);
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка");
                                continue;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Вы в меню");
                            break;
                        }
                    }
                    break;
                //for
                case 4:
                    while (true)
                    {
                        Console.WriteLine("вы выбрали инструкцию For");
                        Console.WriteLine("нажмите Enter для запуска инструкции, для возврата к списку подпрограмм нажмите Esc");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                Console.WriteLine("Введите Начальное число:");
                                int start = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Введите Количество повторений:");
                                int repeats = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Результат:");

                                for (int i = start; i < start + repeats; i++)
                                {
                                    Console.WriteLine(i);
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка");
                                continue;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Вы в меню");
                            break;
                        }
                    }
                    break;
                //foreach
                case 5:
                    while (true)
                    {
                        Console.WriteLine("вы выбрали инструкцию Foreach");
                        Console.WriteLine("нажмите Enter для запуска инструкции, для возврата к списку подпрограмм нажмите Esc");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                Console.WriteLine("Введите числа через пробел для проверки");
                                string[] input = Console.ReadLine().Split(' ');
                                Console.WriteLine("Результат:");
                                foreach (var item in input)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка");
                                continue;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Вы в меню");
                            break;
                        }

                    }
                    break;
                //switch
                case 6:
                    while (true)
                    {
                        Console.WriteLine("вы выбрали инструкцию Switch");
                        Console.WriteLine("нажмите Enter для запуска инструкции, для возврата к списку подпрограмм нажмите Esc");
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            try
                            {
                                Console.WriteLine("Введите номер дня недели");
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
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка");
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Вы в меню");
                            break;
                        }

                    }
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
}
