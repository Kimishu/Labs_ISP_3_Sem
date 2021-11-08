using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zz.Collections;

namespace _053502_ANKUSHEV_LAB5.Entities
{
    public class Journal
    {
        MyCustomCollection<string> Events = new MyCustomCollection<string>();

        public void ShowEvents()
        {
            for(int i= Events.Count-1; i>=0; i--)
            {
                Console.WriteLine(Events[i]);
            }
            Console.WriteLine("===");
        }

        public void AddEvent(string entity, string description)
        {
            Events.Add("===\nEntity name: " + entity + '\n' + description);
        }
    }
}
