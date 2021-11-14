using System;
using System.Collections.Generic;
using System.Threading;

namespace Domain
{
    [Serializable]
    public class Building
    {

        public List<HeatingSystem> heatingSysList = new();

        public string buildingType;
        public string buildingNum;
        public int roomsCount;
        public int floors;
        public int entranceNum;

        public int heatingSystemsCount;

        public Building(string bt,string bn, int rc, int fl, int en, int hsc,List<HeatingSystem> hts)
        {
            buildingType = bt;
            buildingNum = bn;
            roomsCount = rc;
            floors = fl;
            entranceNum = en;
            heatingSystemsCount = hsc;
            heatingSysList = hts;
        }

        public Building()
        {

        }
    }
}
