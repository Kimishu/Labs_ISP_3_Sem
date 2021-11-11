using System;
using System.Collections.Generic;
using System.Text;

namespace zz.Entities
{
    public class Call
    {
        public int seconds;
        public string recipientCity;
        public string senderCity;
        public int callCost;
        public Tariff tariff;
        public Call(int sec, string rec, string send, int cost, Tariff t)
        {
            seconds = sec;
            recipientCity = rec;
            senderCity = send;
            callCost = cost;
            tariff = t;
        }
    }
}
