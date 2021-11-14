using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{ 
    public interface ISerializer
    {
        IEnumerable<Building> DeSerializeByLINQ(string fileName);
        IEnumerable<Building> DeSerializeXML(string fileName);
        IEnumerable<Building> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Building> xxx, string fileName);
        void SerializeXML(IEnumerable<Building> xxx, string fileName);
        void SerializeJSON(IEnumerable<Building> xxx, string fileName);
    }
}
