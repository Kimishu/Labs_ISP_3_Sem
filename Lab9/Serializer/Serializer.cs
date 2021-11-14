using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.Json;
using System.IO;

namespace Serializer
{
    public class Serializer : ISerializer
    {
        public IEnumerable<Building> DeSerializeByLINQ(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            var buildings = (from elements in doc.Descendants("Building")
                             select new Building(
                                 elements.Attribute("type").Value,
                                 elements.Element("number").Value,
                                 Convert.ToInt32(elements.Element("rooms").Value),
                                 Convert.ToInt32(elements.Element("floors").Value),
                                 Convert.ToInt32(elements.Element("entranceNum").Value),
                                 Convert.ToInt32(elements.Element("heatingSystems").Value),
                                 (from hsystems in doc.Descendants("HeatingSystem")
                                  select new HeatingSystem(
                                        Convert.ToInt32(hsystems.Element("maxCelsius").Value),
                                        Convert.ToInt32(hsystems.Element("minCelsius").Value),
                                        hsystems.Element("address").Value,
                                        Convert.ToInt32(hsystems.Element("length").Value))
                                  ).ToList()
                             )).ToList();
            return buildings;
        }

        public IEnumerable<Building> DeSerializeJSON(string fileName)
        {
            using (FileStream fs = new(fileName,FileMode.OpenOrCreate))
            {
                //string file = fs.ReadToEnd();
                var options = new JsonSerializerOptions { IncludeFields = true };
                var collection = JsonSerializer.DeserializeAsync<List<Building>>(fs,options).Result;
                return collection;
            }
        }

        public IEnumerable<Building> DeSerializeXML(string fileName)
        {
            XmlSerializer formatter = new(typeof(List<Building>));
            using (FileStream fs = new(fileName, FileMode.OpenOrCreate))
            {
                var collection = (List<Building>)formatter.Deserialize(fs);
                return collection;
            }
        }

        public void SerializeByLINQ(IEnumerable<Building> buildings, string fileName)
        {
            XDocument xdoc = new();

            XElement BWHS = new("BuildingsWithHeatingSystem");

            foreach(var building in buildings)
            {
                var building1 = new XElement("Building", new[] 
                {
                new XElement("number", building.buildingNum),
                new XElement("rooms", building.roomsCount.ToString()),
                new XElement("floors", building.floors.ToString()),
                new XElement("entranceNum",building.entranceNum.ToString()),
                new XElement("heatingSystems", building.heatingSystemsCount.ToString()),
                });

                building1.Add(new XAttribute("type", building.buildingType));

                foreach(var heatingSys in building.heatingSysList)
                {
                    building1.Add(new XElement("HeatingSystem", new[] {
                        new XElement("address",heatingSys.heatingSystemAddress),
                        new XElement("length",heatingSys.heatingSystemLength.ToString()),
                        new XElement("maxCelsius",heatingSys.maxTemperatureCelsius.ToString()),
                        new XElement("minCelsius",heatingSys.minTemperatureCelsius.ToString())
                    }));
                }

                BWHS.Add(building1);
            }

            xdoc.Add(BWHS);
            xdoc.Save(fileName);

        }

        public void SerializeJSON(IEnumerable<Building> buildings, string fileName)
        {
            using (FileStream fs = new(fileName,FileMode.OpenOrCreate))
            {
                //string json = JsonSerializer.Serialize<List<Building>>(buildings.ToList());
                //fs.Write(json);
                var options = new JsonSerializerOptions { IncludeFields = true };
                JsonSerializer.SerializeAsync<List<Building>>(fs, buildings.ToList(),options).Wait();
            }
        }

        public void SerializeXML(IEnumerable<Building> buildings, string fileName)
        {
            XmlSerializer formatter = new(typeof(List<Building>));

            using (FileStream fs = new(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, buildings.ToList());
            }
        }
    }
}
