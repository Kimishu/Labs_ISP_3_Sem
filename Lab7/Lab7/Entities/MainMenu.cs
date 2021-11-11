using _053502_ANKUSHEV_LAB5.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace zz.Entities
{
    public class MainMenu
    {
        private Journal events = new();
        public static void Variants(int index)
        {
            List<string> VariantsList = new List<string>();
            VariantsList.Add("Сделать звонок");
            VariantsList.Add("Стоимость звонков клиента");
            VariantsList.Add("Общая стоимость звонков АТС");
            VariantsList.Add("Список тарифов(по цене)");
            VariantsList.Add("Вывод клиентов(по сумме)");
            VariantsList.Add("История платежей клиента");
            Console.WriteLine("\t[Главное меню]");
            for (int i = 0; i < VariantsList.Count; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{i+1}." + VariantsList[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i+1}." + VariantsList[i]);
                }
            }
            Console.WriteLine("\tСтраница [1/2]");
        }
        public static void Variants2(int index)
        {
            List<string> VariantsList = new List<string>();
            VariantsList.Add("Создать тариф");
            VariantsList.Add("Добавить клиента");
            VariantsList.Add("Добавить тариф (клиенту)");
            VariantsList.Add("Удалить клиента");
            VariantsList.Add("Показать события");
            Console.WriteLine("\t[Главное меню]");
            for (int i = 0; i < VariantsList.Count; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{i + 1}." + VariantsList[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1}." + VariantsList[i]);
                }
            }
            Console.WriteLine("\tСтраница [2/2]");
        }
        public void Menu(ATE at)
        {

            at.ListChangesEvent += (entity,msg) => events.AddEvent(entity,msg);

            at.CreateTariffs();
            int count = 0;
            bool check = false;
            Variants(count);
            while (true)
            {

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (!check)
                        {
                            if (count == 5)
                            {
                                count = 0;
                            }
                            else
                            {
                                count++;
                            }
                            Variants(count);
                        }
                        if (check)
                        {
                            if (count == 4)
                            {
                                count = 0;
                            }
                            else
                            {
                                count++;
                            }
                            Variants2(count);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (!check)
                        {
                            if (count == 0)
                            {
                                count = 5;
                            }
                            else
                            {
                                count--;
                            }
                            Variants(count);
                        }
                        if (check)
                        {
                            if (count == 0)
                            {
                                count = 4;
                            }
                            else
                            {
                                count--;
                            }
                            Variants2(count);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        Console.Clear();
                        count = 0;
                        if (check.Equals(true))
                        {
                            check = false;
                            Variants(count);
                        }
                        else
                        {
                            check = true;
                            Variants2(count);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.Clear();
                        count = 0;
                        if (check.Equals(false))
                        {
                            check = true;
                            Variants2(count);
                        }
                        else {
                            check = false;
                            Variants(count);
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (!check)
                        {
                            switch (count)
                            {
                                case 0:
                                    at.ShowClients();
                                    Console.WriteLine("\nВведите номер клиента, совершившего звонок!");
                                    int clientNumber1 = Convert.ToInt32(Console.ReadLine());
                                    if (clientNumber1 > at.ClientsList.Count || clientNumber1 <= 0)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Что-то не так!");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        at.ClientsList[clientNumber1 - 1].MakeCall(at,at.ClientsList[clientNumber1 - 1]);
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    break;
                                case 1:
                                    at.ShowClients();
                                    Console.WriteLine("\nВведите номер клиента, чтобы узнать стоимость его звонков!\nИли любую другую клавишу для выхода");
                                    string clientNumber2 = Console.ReadLine();
                                    int val = 0;
                                    if (int.TryParse(clientNumber2, out val))
                                    {
                                        Console.Clear();
                                        at.TotalClientCost(val-1);
                                        Console.WriteLine("\n\nНажмите любую кнопку, чтобы продолжить!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        string outstr = "Возвращаемся";
                                        for (int i = 0; i < 3; i++)
                                        {
                                            outstr += '.';
                                            Console.WriteLine(outstr);
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    break;
                                case 2:
                                    at.TotalCost();
                                    at.GetTopClientCost();
                                    Console.WriteLine("\n\nНажмите любую кнопку, чтобы продолжить!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case 3:
                                    at.ShowTariffsSortedByCost();
                                    Console.WriteLine("\n\nНажмите любую кнопку, чтобы продолжить!");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case 4:
                                    Console.WriteLine("\nВведите сумму($)!\nИли любую другую клавишу для выхода");
                                    string cost = Console.ReadLine();
                                    int sum = 0;
                                    if (int.TryParse(cost, out sum))
                                    {
                                        Console.Clear();
                                        at.GetClientsFromCost(sum);
                                        Console.WriteLine("\n\nНажмите любую кнопку, чтобы продолжить!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        string outstr = "Возвращаемся";
                                        for (int i = 0; i < 3; i++)
                                        {
                                            outstr += '.';
                                            Console.WriteLine(outstr);
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    break;
                                case 5:
                                    at.ShowClients();
                                    Console.WriteLine("\nВведите номер клиента, чтобы узнать суммы, заплаченные по каждому тарифу!\nИли любую другую клавишу для выхода");
                                    string clientNumber3 = Console.ReadLine();
                                    int val2 = 0;
                                    if (int.TryParse(clientNumber3, out val2))
                                    {
                                        Console.Clear();
                                        at.GetSumfListEachTariff(val2-1);
                                        Console.WriteLine("\n\nНажмите любую кнопку, чтобы продолжить!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        string outstr = "Возвращаемся";
                                        for (int i = 0; i < 3; i++)
                                        {
                                            outstr += '.';
                                            Console.WriteLine(outstr);
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    break;
                            }
                        }
                        else 
                        {
                            switch (count)
                            {
                                case 0://createtariff
                                    at.CreateTariff();
                                    Console.Clear();
                                    break;
                                case 1:
                                    at.AddClient();
                                    Console.Clear();
                                    break;
                                case 2://add tariff to client
                                    at.ShowClients();
                                    Console.WriteLine("\nВведите номер клиента, которому хотите добавить тариф!\nИли любую другую клавишу для выхода");
                                    string clientNumber = Console.ReadLine();
                                    int val = 0;
                                    if (int.TryParse(clientNumber, out val))
                                    {
                                        Console.Clear();
                                        at.ClientsList[val - 1]?.AddTariff(at);
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        string outstr = "Возвращаемся";
                                        for (int i = 0; i < 3; i++)
                                        {
                                            outstr += '.';
                                            Console.WriteLine(outstr);
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    break;
                                case 3://del client
                                    at.ShowClients();
                                    Console.WriteLine("\nВведите номер клиента, которого желаете удалить\n");
                                    string clientNumberToDel = Console.ReadLine();
                                    int val2 = 0;
                                    if (int.TryParse(clientNumberToDel, out val2))
                                    {
                                        Console.Clear();
                                        at.RemoveClient(val2 - 1);
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        string outstr = "Возвращаемся";
                                        for (int i = 0; i < 3; i++)
                                        {
                                            outstr += '.';
                                            Console.WriteLine(outstr);
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                    break;
                                case 4://show events
                                    events.ShowEvents();

                                    Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться.");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                            }
                        }
                        if(!check) Variants(count);
                        else Variants2(count);
                        break;
                }
            }
        }
    }
}
