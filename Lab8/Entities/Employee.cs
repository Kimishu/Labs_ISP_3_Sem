using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Entities
{
    public class Employee
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public bool Working { get; set; }

        public Employee(string name, int age, bool working)
        {
            Name = name;
            Age = age;
            Working = working;
        }
        
    }
}
