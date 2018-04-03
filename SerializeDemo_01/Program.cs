using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SerializeDemo_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var objectGraph = new List<string> { "tj1", "tj2", "tj3" };
            Stream stream = SerializeToMemory(objectGraph);

            stream.Position = 0;
            objectGraph = null;

            objectGraph = (List<string>)DeserializeFromMemory(stream);

            foreach (var item in objectGraph)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();

            string str = Encoding.Default.GetString((stream as MemoryStream).GetBuffer());
            Console.WriteLine(str);
            Console.ReadKey();
        }

        static MemoryStream SerializeToMemory(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            return memoryStream;
        }

        static object DeserializeFromMemory(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
