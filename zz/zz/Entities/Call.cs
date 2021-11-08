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
        public Call(int sec, string rec, string send)
        {
            seconds = 0;
            recipientCity = "";
            senderCity = "";
        }
    }
}
