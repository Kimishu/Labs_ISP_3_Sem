using System;
using System.Collections.Generic;
using System.IO;
using Lab8.Interfaces;

namespace Lab8.Entities
{
    class FileService :IFileService
    {
        
        public IEnumerable<Employee> ReadFile(string fileName)
        {
            string path = $"../../{fileName}.txt";
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    int age = reader.ReadInt32();
                    bool hasWork = reader.ReadBoolean();

                    yield return new Employee(name, age, hasWork);
                }

                reader.Close();
            }
        }
        public void SaveData(IEnumerable<Employee> data, string fileName)
        {
            string path = $"../../{fileName}.txt";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                foreach (var human in data)
                {
                    writer.Write(human.Name);
                    writer.Write(human.Age);
                    writer.Write(human.Working);
                }

                writer.Close();
            }

        }
    }
}
