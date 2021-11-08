using System;
using System.Threading;
using zz.Collections;

namespace zz.Entities
{
    public class ATE
    {
        public static MyCustomCollection<Client> ClientsList = new MyCustomCollection<Client>();
        public static MyCustomCollection<Tariff> TariffList = new MyCustomCollection<Tariff>();
        public static MyCustomCollection<Call> ClientsCalls = new MyCustomCollection<Call>();

        public delegate void ListChangesDelegate(string entity,string description);
        public event ListChangesDelegate ListChangesEvent;

        public delegate void CallEventDelegate(string entity, string description);
        public event CallEventDelegate CallEvent;
            
        public void CreateTariffs()
        {
            TariffList.Add(new Tariff("Токио", 5));
            TariffList.Add(new Tariff("Канагава", 6));
            TariffList.Add(new Tariff("Асахикава", 7));
            for (int i = TariffList.Count - 1; i >= 0; i--)
            {
                ListChangesEvent?.Invoke(TariffList[i].tariffName, "Добавлен новый тариф!");
            }
        }
        public void CreateTariff()
        {
            Console.WriteLine("Введите город: ");
            string tariffName = Console.ReadLine();
            Console.WriteLine("Введите цену тарифа($): ");
            int tariffCost = Convert.ToInt32(Console.ReadLine());
            TariffList.Add(new Tariff(tariffName, tariffCost));

            ListChangesEvent?.Invoke(TariffList[TariffList.Count - 1].tariffName, "Добавлен новый тариф!");
        }
        public static void AvailableTariffs()
        {

            for (int i = 0; i < TariffList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Город: " + TariffList[i].tariffName + " Цена: " + TariffList[i].tariffCost + " $");
            }
        }
        public void ShowClients()
        {
            for (int i = 0; i < ClientsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Имя: " + ClientsList[i].name + "\nФамилия: " + ClientsList[i].surname + "\nГород: "+ ClientsList[i].clientCity);
            }
        }
        public Tariff FindTariffByName(string tariffCity)
        {
            for (int i = 0; i < TariffList.Count; i++)
            {
                if (TariffList[i].tariffName.Equals(tariffCity))
                {
                    return TariffList[i];
                }
            }
            CreateTariff();
            return TariffList[TariffList.Count - 1];
        }
        public static Tariff FindTariffByIndex(int index)
        {
            while (true)
            {
                if (index < 0 || index > TariffList.Count)
                {
                    Console.WriteLine("Неверный ввод!");
                    index = Convert.ToInt32(Console.ReadLine());
                    continue;
                }
                else
                {
                    return TariffList[index];
                }
            }
        }
        public void AddClient()
        {
            ClientsList.Add(new Client());

            ListChangesEvent?.Invoke(ClientsList[ClientsList.Count-1].name + ' '+ ClientsList[ClientsList.Count - 1].surname, "Добавлен новый клиент!");
        }

        public static void RemoveClient(int index)
        {
            ClientsList.Remove(ClientsList[index]);
        }
        public void MakeCall(string recipientCity, string clientCity, ref int callsCost)
        {
            Random rnd = new Random();
            if (Compare(recipientCity))
            {
                Client.callsNumber++;
                if (recipientCity.Equals(clientCity))
                {
                    callsCost += FindTariffByName(recipientCity).tariffCostSec;
                }
                else
                {
                    callsCost += FindTariffByName(recipientCity).tariffCost;
                }
                Client.calls.Add(new Call(rnd.Next(100, 600), recipientCity, clientCity));
                ClientsCalls.Add(Client.calls.Current());
                Console.WriteLine("Звонок успешно совершён!");
                CallEvent?.Invoke("Из: "+clientCity+'\n'+"Кому: "+recipientCity+'\n',"Совершён звонок!");
            }
            if (recipientCity.Equals(0) || !Compare(recipientCity))
            {
                Console.WriteLine("Неверный ввод.\n");
                Thread.Sleep(1500);
                Console.Clear();
            }

        }
        public static bool Compare(string city)
        {
            for (int i = 0; i < TariffList.Count; i++)
            {
                if (TariffList[i].tariffName.Equals(city))
                {
                    return true;
                }
            }
            return false;
        }
        public void TotalClientCost()
        {
            while (true)
            {
                for (int i = 0; i < ClientsList.Count; i++)
                {
                    Console.WriteLine($"{ i + 1}. {ClientsList[i].name}"+" "+ $"{ClientsList[i].surname}");
                }
                Console.WriteLine("Введите номер клиента:");
                int index = Convert.ToInt32(Console.ReadLine())-1;
                if (index >= 0 && index <= ClientsList.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Общая стоимость всех выполненных звонков\nклиента {ClientsList[index].name}"+ " " + $"{ClientsList[index].surname} равна: {ClientsList[index].callsCost}$");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Что-то не так!");
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            }
        }
        public void TotalCost()
        {
            int totalCost = 0;
            for(int i=0; i<ClientsList.Count; i++)
            {
                totalCost += ClientsList[i].callsCost;
            }
            Console.WriteLine($"Общая стоимость всех выполненных звонков: {totalCost}$");
        }
    }
}
