using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinarySerialization
{
    [Serializable]
    public class SerializableClass
    {
        [NonSerialized] public int code; //skips serializing
        public string city;
    }
    class Program
    {
        static void Main(string[] args)
        {
            SerializableClass myObject = new SerializableClass { code = 560072, city = "bengaluru" };
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("D:\\MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, myObject);
            stream.Close();

            stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            SerializableClass obj = (SerializableClass)formatter.Deserialize(stream);
            stream.Close();
        }
    }
}
