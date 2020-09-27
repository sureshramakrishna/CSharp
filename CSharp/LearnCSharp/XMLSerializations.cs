using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLSerializations
{
    [Serializable]
    public class SerializableClass
    {
        public int code;
        public string city;
    }
    class Program
    {
        static void Main(string[] args)
        {
            SerializableClass myObject = new SerializableClass { code = 560072, city = "bengaluru" };

            XmlSerializer mySerializer = new XmlSerializer(typeof(SerializableClass));
            StreamWriter myWriter = new StreamWriter("D:\\myFileName.xml");
            mySerializer.Serialize(myWriter, myObject);
            myWriter.Close();

            FileStream fileStream = new FileStream("D:\\myFileName.xml", FileMode.Open);
            myObject = (SerializableClass)mySerializer.Deserialize(fileStream);
        }
    }
}
