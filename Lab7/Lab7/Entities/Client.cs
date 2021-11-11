using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace zz.Entities
{
    public class Client
    {
        private static List<Tariff> usertariffs = new();
        public List<Call> calls = new();
        public string name;
        public string surname;
        public string clientCity;
        public static int callsNumber;
        public int callsCost;
        public Client()
        {
            Console.WriteLine("Введите имя и фамилию");
            name = Console.ReadLine();
            surname = Console.ReadLine();
            Console.WriteLine("Введите ваш город:");
            clientCity = Console.ReadLine();
            callsNumber = 0;
            callsCost = 0;
            Console.Clear();
        }

        public void AddTariff(ATE at)
        {
            at.AvailableTariffs();
            Console.WriteLine("\nВведите номер тарифа:");
            int tariffNum = Convert.ToInt32(Console.ReadLine());

            usertariffs.Add(at.FindTariffByIndex(tariffNum-1));
            Console.Clear();
        }
        public void MakeCall(ATE at,Client cl)
        {
            string recipientCity;
            bool check = false;
            at.AvailableTariffs();
            Console.WriteLine();
            Console.WriteLine("Введите номер города получателя(или 0 для отмены):");

            recipientCity = Console.ReadLine();
            int output = 0;

            if (int.TryParse(recipientCity, out output))
            {
                --output;
                Console.Clear();
                Tariff t = at.FindTariffByIndex(output);
                for (int j = 0; j < usertariffs.Count; j++)
                {
                    if (usertariffs[j].tariffName.Equals(t.tariffName))
                    {
                        check = true;
                    }
                }
                if (check.Equals(false))
                {
                    Console.Clear();
                    Console.WriteLine("У вас отсутствует тариф!\n");
                    AddTariff(at);
                }
                else
                {
                    at.MakeCall(at.FindTariffByIndex(output).tariffName, clientCity, ref callsCost, cl);
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода!");
                Thread.Sleep(1500);
                Console.Clear();
            }
        }
        public void CheckCalls()
        {
            for(int i=0; i< calls.Count; i++)
            {
                Console.WriteLine("================================");
                Console.WriteLine($"{i+1}. Длительность: {calls[i].seconds}\nОт: {calls[i].senderCity}\nКому: {calls[i].recipientCity} ");
            }

            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
