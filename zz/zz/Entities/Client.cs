using System;
using System.Collections.Generic;
using System.Text;
using zz.Collections;


namespace zz.Entities
{
    public class Client
    {
        private static MyCustomCollection<Tariff> usertariffs = new MyCustomCollection<Tariff>();
        public static MyCustomCollection<Call> calls = new MyCustomCollection<Call>();
        public string name;
        public string surname;
        public string clientCity;
        public static int callsNumber;
        public int callsCost;
        public Client()
        {
            Random rnd = new Random();
            Console.WriteLine("Введите имя и фамилию");
            name = Console.ReadLine();
            surname = Console.ReadLine();

            Console.WriteLine("Введите ваш город:");
            clientCity = Console.ReadLine();
            usertariffs.Add(new Tariff(clientCity, rnd.Next(1, 8)));
            callsNumber = 0;
            callsCost = 0;
            Console.Clear();
        }
        public void AddTariff()
        {
            ATE.AvailableTariffs();
            Console.WriteLine("\nВведите номер тарифа:");
            int tariffNum = Convert.ToInt32(Console.ReadLine());

            usertariffs.Add(ATE.FindTariffByIndex(tariffNum-1));
            Console.Clear();
        }
        public void MakeCall(ATE at)
        {
            string recipientCity;
            bool check = false;
            Console.WriteLine("Список городов:");
                for (int i = 0; i < ATE.TariffList.Count; i++)
                {
                    Console.WriteLine(ATE.TariffList[i].tariffName);
                }
            Console.WriteLine();
            Console.WriteLine("Введите город получателя(или 0 для отмены):");
            recipientCity = Console.ReadLine();
            for (int i = 0; i < usertariffs.Count; i++)
            {
                if (usertariffs[i].tariffName.Equals(recipientCity))
                {
                    check = true;
                }
            }
            if (!check)
            {
                Console.Clear();
                Console.WriteLine("У вас отсутствует тариф!\n");
                AddTariff();
            }
            else
            {
                at.MakeCall(recipientCity, clientCity, ref callsCost);
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
