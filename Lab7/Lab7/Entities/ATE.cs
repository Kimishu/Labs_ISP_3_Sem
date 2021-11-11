using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace zz.Entities
{
    public class ATE
    {
        public List<Client> ClientsList = new();
        public Dictionary<string,Tariff> TariffList = new();
        public List<Call> ClientsCalls = new();

        public delegate void ListChangesDelegate(string entity,string description);
        public event ListChangesDelegate ListChangesEvent;

        public delegate void CallEventDelegate(string entity, string description);
        public event CallEventDelegate CallEvent;
            
        public void CreateTariffs()
        {
            InsertTariff("Япония1", new Tariff("Токио", 5));
            InsertTariff("Япония2", new Tariff("Канагава", 6));
            InsertTariff("Япония3", new Tariff("Асахикава", 7));
        }

        public void InsertTariff(string name, Tariff value)
        {
            try
            {
                this.TariffList.Add(name, value);
                this.ListChangesEvent?.Invoke(name, "Добавлен новый тариф!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Неверное выражение!");
                CreateTariff();
            }
        }

        public void CreateTariff()
        {
            Console.WriteLine("Введите страну: ");
            string tariffCountry = Console.ReadLine();
            Console.WriteLine("Введите город: ");
            string tariffName = Console.ReadLine();
            Console.WriteLine("Введите цену тарифа($): ");
            int tariffCost = Convert.ToInt32(Console.ReadLine());

            InsertTariff(tariffCountry, new Tariff(tariffName, tariffCost));
        }

        public void ShowTariffsSortedByCost()
        {
            var sortedDictionary = from tariff in TariffList.Values
                                   orderby tariff.tariffCost
                                   select tariff;
            int i = 0;
            foreach (var keyValue in sortedDictionary)
            {
                //Console.WriteLine($"{++i}. Город: {keyValue.tariffName} Цена: {keyValue.tariffCost} $");
                Console.WriteLine($"{++i,-2}| Город:{keyValue.tariffName,-16}| Цена:{keyValue.tariffCost,-4}$");
            }
        }
        public void AvailableTariffs()
        {
            Console.WriteLine("\t[Список городов]");
            int i = 0;
            foreach (var city in TariffList.Values)
            {
                Console.WriteLine($"{++i}. {city.tariffName}");
            }
        }
        public void ShowClients()
        {
            Console.WriteLine("\t[Список клиентов]");
            int i = 0;
            foreach(var clients in ClientsList)
            {
                Console.WriteLine($"{++i}. Имя: {clients.name}");
                Console.WriteLine("{0,-3}",$"Фамилия: {clients.surname}");
                Console.WriteLine("{0,-3}",$"Город: {clients.clientCity}");
            }
        }
        public Tariff FindTariffByName(string tariffCity)
        {
            foreach(var tariff in TariffList.Values)
            {
                if (tariff.tariffName.Equals(tariffCity))
                {
                    return tariff;
                }
            }
            CreateTariff();
            return TariffList.Values.Last();
        }
        public Tariff FindTariffByIndex(int index)
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
                    Tariff tariff = TariffList.ElementAt(index).Value;
                    return tariff;
                }
            }
        }
        public void AddClient()
        {
            ClientsList.Add(new Client());

            ListChangesEvent?.Invoke(ClientsList[ClientsList.Count-1].name + ' '+ ClientsList[ClientsList.Count - 1].surname, "Добавлен новый клиент!");
        }

        public void RemoveClient(int index)
        {
            ClientsList.Remove(ClientsList[index]);
        }
        public void MakeCall(string recipientCity, string clientCity, ref int callsCost,Client cl)
        {
            Random rnd = new();
            Tariff t = FindTariffByName(recipientCity);
            if (Compare(recipientCity))
            {
                Client.callsNumber++;
                if (recipientCity.Equals(clientCity))
                {

                    callsCost += t.tariffCostSec;
                    cl.calls.Add(new Call(rnd.Next(100, 600), recipientCity, clientCity, t.tariffCostSec,t));
                }
                else
                {
                    callsCost += t.tariffCost;
                    cl.calls.Add(new Call(rnd.Next(100, 600), recipientCity, clientCity,t.tariffCost,t));
                }
                ClientsCalls.Add(cl.calls.Last());
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
        public bool Compare(string city)
        {
            foreach(var keyValue in TariffList.Values)
            {
                if (keyValue.tariffName.Equals(city)) return true;
            }

            return false;
        }
        public void TotalClientCost(int index)
        {
            while (true)
            {
                if (index >= 0 && index < ClientsList.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Общая стоимость всех выполненных звонков\nклиента {ClientsList[index].name} {ClientsList[index].surname} равна: {ClientsList[index].callsCost}$");
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неверный ввод");
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            }
        }
        public void TotalCost()
        {
            int totalCost = 0;

            foreach (Client cost in ClientsList)
            {
                totalCost += cost.callsCost;
            }

            Console.WriteLine($"Общая стоимость всех выполненных звонков: {totalCost}$");
        }

        public void GetTopClientCost()
        {
            var client = from b in ClientsList
                         where b.callsCost.Equals(ClientsList.Max((Client) => Client.callsCost))
                         select b;
            Console.WriteLine("Заплатили максимальную сумму:");
            int i = 0;
            foreach (Client b in client)
            {
                Console.WriteLine($"{++i}. Имя:{b.name} Фамилия:{b.surname} Сумма:{b.callsCost}");
            }
        }

        public void GetClientsFromCost(int cost)
        {
            var client = ClientsList.Aggregate(cost,(total,next)=>next.callsCost > cost ? ++total : total);

            Console.WriteLine($"Заплатили максимальную сумму:{0}", client);
        }

        public void GetSumfListEachTariff(int index)
        {
            var filteredSum = from calls in ClientsList[index].calls
                              group calls by calls.tariff;

             Dictionary<string, int> sumList = new();
            int sum = 0;

            foreach (var tariff in TariffList.Values) {
                foreach (var igroup in filteredSum) {

                    if (tariff.Equals(igroup.Key))
                    {
                        foreach (var call in igroup)
                        {
                            sum += call.callCost;
                        }

                    }
                }
                sumList.Add(tariff.tariffName, sum);
                sum = 0;
            }

            int i = 0;
            foreach (var tariffSum in sumList)
            {
                Console.WriteLine($"{++i,-2}|Тариф: {tariffSum.Key,-16} | Сумма: {tariffSum.Value,-4} $");
               // Console.WriteLine($"{++i,-2}| Город:{keyValue.tariffName,-16}| Цена:{keyValue.tariffCost,-4}$");
            }
        }
    }
}
