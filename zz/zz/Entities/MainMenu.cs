using _053502_ANKUSHEV_LAB5.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using zz.Collections;
namespace zz.Entities
{
    public class MainMenu
    {
        private Journal events = new();
        public static void Variants(int index)
        {
            MyCustomCollection<string> VariantsList = new MyCustomCollection<string>();
            VariantsList.Add("Добавить клиента");
            VariantsList.Add("Добавить тариф");
            VariantsList.Add("Сделать звонок");
            VariantsList.Add("Стоимость звонков клиента");
            VariantsList.Add("Общая стоимость звонков АТС");
            VariantsList.Add("Удалить клиента");
            VariantsList.Add("Показать события");
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
        }
        public void Menu(ATE at)
        {

            at.ListChangesEvent += (entity,msg) => events.AddEvent(entity,msg);

            at.CreateTariffs();
            int count = 0;
            Variants(count);
            while (true)
            {
                
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.Clear();
                        if (count == 6)
                        {
                            count = 0;
                        }
                        else
                        {
                            count++;
                        }
                        Variants(count);
                        break;
                    case ConsoleKey.UpArrow:
                        Console.Clear();
                        if (count == 0)
                        {
                            count = 6;
                        }
                        else
                        {
                            count--;
                        }
                        Variants(count);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (count)
                        {
                            case 0:
                                at.AddClient();
                                Console.Clear();
                                break;
                            case 1:
                                 at.ShowClients();
                                 Console.WriteLine("\nВведите номер клиента, которому хотите добавить тариф!\nИли любую другую клавишу для выхода");
                                 string clientNumber = Console.ReadLine();
                                 int val = 0;
                                    if (int.TryParse(clientNumber,out val))
                                    {
                                        Console.Clear();
                                        ATE.ClientsList[val - 1]?.AddTariff();
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        string outstr = "Возвращаемся";
                                        for(int i=0; i<3; i++)
                                        {
                                            outstr += '.';
                                            Console.WriteLine(outstr);
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                        break;
                                    }
                            case 2:
                                at.ShowClients();
                                Console.WriteLine("\nВведите номер клиента, совершившего звонок!");
                                int number = Convert.ToInt32(Console.ReadLine());
                                    if (number > ATE.ClientsList.Count || number <= 0)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Что-то не так!");
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        ATE.ClientsList[number-1].MakeCall(at);
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                        break;
                                    }
                            case 3:
                                at.TotalClientCost();
                                break;
                            case 4:
                                at.TotalCost();
                                Console.WriteLine("\nНажмите любую кнопку, чтобы продолжить!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case 5:
                                at.ShowClients();
                                Console.WriteLine("\nВведите номер клиента, которого желаете удалить\n");
                                string clientNumberToDel = Console.ReadLine();
                                int val2 = 0;
                                if (int.TryParse(clientNumberToDel, out val2))
                                {
                                    Console.Clear();
                                    ATE.RemoveClient(val2 - 1);
                                    Thread.Sleep(1500);
                                    Console.Clear();
                                    break;
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
                                    break;
                                }
                            case 6:
                                events.ShowEvents();

                                Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться.");
                                Console.ReadKey();
                                Console.Clear();

                                break;
                        }
                        Variants(count);
                        break;
                }
            }
        }
    }
}
