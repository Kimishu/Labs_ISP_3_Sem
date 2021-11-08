using System;
using System.Collections.Generic;
using System.Text;

namespace zz.Entities
{
    public class Tariff
    {
        public string tariffName;
        public int tariffCost;
        public int tariffCostSec;
        public Tariff(string tn,int tc)
        {
            tariffName = tn;
            tariffCost = tc;
            tariffCostSec = 3;
        }

    }
}
