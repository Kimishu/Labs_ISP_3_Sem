using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class HeatingSystem
    {
        public string heatingSystemAddress;

        public int heatingSystemLength;

        public int maxTemperatureCelsius;

        public int minTemperatureCelsius;

        public HeatingSystem(int maxt, int mint, string hssa, int hlength)
        {
            maxTemperatureCelsius = maxt;
            minTemperatureCelsius = mint;
            heatingSystemAddress = hssa;
            heatingSystemLength = hlength;
        }

        public HeatingSystem()
        {

        }

    }
}
