using Domain;
using Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _053502_ANKUSHEV_LAB9
{
    class Menu
    {
        void OutputCollection(string text,List<Building> smth)
        {
            int i = 0;
            Console.WriteLine(text);
            foreach(var b in smth)
            {
                Console.WriteLine("==============");
                Console.WriteLine($"{++i}\nТип здания:{b.buildingType}\nНомер здания:{b.buildingNum}\n" +
                    $"Количество комнат:{b.roomsCount}\nКоличество этажей:{b.floors}\nКоличество входов:{b.entranceNum}" +
                    $"\nКоличество отопительных систем:{b.heatingSystemsCount}");
                //int j = 0;
                //Console.WriteLine("\t[Отопительные системы]");
                //foreach (var s in b.heatingSysList)
                //{
                //    Console.WriteLine($"{++j}\nНомер отопительной системы:{s.heatingSystemAddress}\n" +
                //        $"Протяжённость отопительной системы:{s.heatingSystemLength}\n" +
                //        $"Максимальная температура:{s.maxTemperatureCelsius}\n" +
                //        $"Минимальная температура:{s.minTemperatureCelsius}");
                //}
            }
        }
        void Variants(int index)
        {
            List<string> menu = new();
            
            menu.Add("Создать коллекцию");
            menu.Add("Сериализация by LINQ");
            menu.Add("Сериализация by JSON");
            menu.Add("Сериализация by Xml.Serialization");
            menu.Add("Десериализация by LINQ");
            menu.Add("Десериализация by JSON");
            menu.Add("Десериализация by Xml.Serialization");
           
            Console.WriteLine("\t[Главное меню]");
            for (int i = 0; i < menu.Count; i++)
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

        public void MainMenu()
        {
            Serializer.Serializer ser = new();
            List<Building> collectionOld = new();
            List<Building> collectionNew = new();
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
                                Random rnd = new();
                                string[] buildingTypes =
                                {
                                    "Овощебаза",
                                    "Кинотеатр",
                                    "Многоэтажный дом",
                                    "Частный дом",
                                    "Завод",
                                    "Ферма"
                                }; 
                                for (int i = 0; i < 5; i++) {
                                    List<HeatingSystem> hts = new();
                                    int hsc = rnd.Next(1, 5);
                                    for(int j=0; j< hsc; j++)
                                    {
                                        hts.Add(new HeatingSystem(rnd.Next(45, 65), rnd.Next(20, 40),Convert.ToString(i+1), rnd.Next(100, 551)));
                                    }
                                    collectionOld.Add(new Building(buildingTypes[rnd.Next(0,5)], $"{i+1}", rnd.Next(1,11), rnd.Next(1,15), rnd.Next(1,6), hsc, hts));
                                }
                                Console.WriteLine("Коллекция создана!");
                                Thread.Sleep(2500);
                                break;

                            case 1:
                                ser.SerializeByLINQ(collectionOld, "Input1");
                                break;

                            case 2:
                                ser.SerializeJSON(collectionOld, "Input2");
                                break;

                            case 3:
                                ser.SerializeXML(collectionOld, "Input3");
                                break;

                            case 4:
                                collectionNew = (ser.DeSerializeByLINQ("Input1")).ToList();
                                OutputCollection("\t[DeSerializeByLINQ]", collectionNew);
                                Console.WriteLine("\n\nНажмите кнопку, чтобы продолжить.");
                                Console.ReadKey();
                                break;

                            case 5:
                                 collectionNew = (ser.DeSerializeJSON("Input2")).ToList();
                                OutputCollection("\t[DeSerializeJSON]", collectionNew);
                                Console.WriteLine("\n\nНажмите кнопку, чтобы продолжить.");
                                Console.ReadKey();
                                break;
                            case 6:
                                 collectionNew = (ser.DeSerializeXML("Input3")).ToList();
                                OutputCollection("\t[DeSerializeXML]", collectionNew);
                                Console.WriteLine("\n\nНажмите кнопку, чтобы продолжить.");
                                Console.ReadKey();
                                break;
                            
                        }
                        Console.Clear();
                        Variants(count);
                        break;
                        

                        
                }
            }
        }
    }
}
