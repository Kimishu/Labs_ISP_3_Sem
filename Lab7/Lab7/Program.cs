using _053502_ANKUSHEV_LAB5.Entities;
using System;
using System.Collections.Generic;
using zz.Entities;

namespace zz
{
    class Program
    {
        private static MainMenu mm = new();
        
        static void Main()
        {
            Journal events = new();
            ATE at = new();

            at.CallEvent += (entity,msg) => events.AddEvent(entity,msg);

            mm.Menu(at);
        }
    }
}
