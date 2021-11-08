using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053502_ANKUSHEV_LAB5.Entities
{
    public class NoItemExistException: Exception
    { 
        public NoItemExistException(string message): base(message) 
        { }

    }
}
