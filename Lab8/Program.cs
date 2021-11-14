using Lab8.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Lab8
{
    class Program
    {
        public static List<Employee> employees = new();
        public static List<Employee> employeesSorted = new();

        static void Menu(int index)
        {
            List<string> menu = new();

            menu.Add("Добавить работника");
            menu.Add("Запись в файл");
            menu.Add("Считка из файла (+сортировка)");
            menu.Add("Вывод коллекций");
            Console.WriteLine("\t[Главное меню]");
            for(int i=0; i<menu.Count; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{i + 1}." + menu[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}." + menu[i]);
                }
            }
        }
       static void Main()
       {
            string inputName = "Input";
            string oldPath = $"../../{inputName}.txt";

            string outputName = "Output";
            string newPath = $"../../{outputName}.txt";

            FileService fs = new();
            int count = 0;
            Menu(count);
            while (true)
            {

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if(count == 3)
                        {
                            count = 0;
                        }
                        else
                        {
                            count++;
                        }
                        Menu(count);
                        break;

                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (count == 0)
                        {
                            count = 3;
                        }
                        else
                        {
                            count--;
                        }
                        Menu(count);
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (count)
                        {
                            case 0:
                                bool flag = true;
                                Console.WriteLine("Введите имя работника: ");
                                string name = Console.ReadLine();
                                Console.Clear();
                                int age = 0;
                                bool hasWork = false;
                                while (true) {
                                    Console.WriteLine("Введите возраст работника:");
                                    string value = Console.ReadLine();
                                    
                                    if(int.TryParse(value, out age))
                                    {
                                        Console.Clear();
                                        break;
                                    }
                                else
                                    {
                                        Console.WriteLine("Неверный ввод!");
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                }
                                while (flag)
                                {
                                    Console.WriteLine("Работает?\nВведите 'Да' либо 'Нет'");                              
                                    switch (Console.ReadLine())
                                    {
                                        case "Да":
                                        case "да":
                                        case "+":
                                            flag = false;
                                            Console.Clear();
                                            hasWork = true;
                                            break;

                                        case "Нет":
                                        case "нет":
                                        case "-":
                                            Console.Clear();
                                            flag = false;
                                            break;
                                        default:
                                            Console.WriteLine("Неверный ввод, повторите попытку.");
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                            break;
                                    }
                                }

                                employees.Add(new Employee(name,age,hasWork));
                                break;
                            case 1:
                                if (File.Exists(oldPath))
                                {
                                    File.Delete(oldPath);
                                    Console.WriteLine($"Файл {inputName}.txt удалён!");
                                }
                                if (File.Exists(newPath))
                                {
                                    File.Delete(newPath);
                                    Console.WriteLine($"Файл {outputName}.txt удалён!");
                                }
                                Console.WriteLine($"Создаём файл {inputName}.txt");
                                fs.SaveData(employees, inputName);
                                Console.WriteLine("Запись в файл выполнена.");
                                File.Move(oldPath, newPath);
                                Console.WriteLine($"Файл переименован в {outputName}.txt");
                                Thread.Sleep(4500);
                                break;
                            case 2:
                                IEnumerable<Employee> employeesUnsorted = fs.ReadFile(outputName);
                                if (employeesUnsorted.Count().Equals(0))
                                {
                                    Console.WriteLine("Список пуст!");
                                    Thread.Sleep(2000);
                                    break;
                                }
                                else
                                {

                                    foreach (var employee in employeesUnsorted)
                                    {
                                        employeesSorted.Add(employee);
                                    }

                                    employeesSorted.Sort(new EmployeeComparer());
                                    Console.WriteLine("Чтение и сортировка завершены!");
                                    Thread.Sleep(2500);
                                    break;
                                }
                            case 3:
                                Console.WriteLine("[Несортированный список]");
                                Console.WriteLine("__________________________________________");
                                Console.WriteLine($"Номер|{"Имя",- 16}|{"Возраст",- 4}|{"Работает",-4}");
                                Console.WriteLine("__________________________________________");
                                int i = 0;
                                foreach(var b in employees)
                                {
                                    Console.WriteLine($"{++i,-5}|{b.Name,-16}|{b.Age,-4}|{b.Working,-4}");
                                }

                                Console.WriteLine("[Сортированный список]");
                                Console.WriteLine("__________________________________________");
                                Console.WriteLine($"Номер|{"Имя",-16}|{"Возраст",-4}|{"Работает",-4}");
                                Console.WriteLine("__________________________________________");
                                int j = 0;
                                foreach (var c in employeesSorted)
                                {
                                    Console.WriteLine($"{++j,-4}|{c.Name,-16}|{c.Age,-4}|{c.Working,-4}");
                                }
                                Console.WriteLine("\n\nЗавершено!\nНажмите любую кнопку, чтобы продолжить.");
                                Console.ReadKey();
                                break;
                        }
                        Console.Clear();
                        Menu(count);
                        break;       
                }
            }
        }
    }
}
