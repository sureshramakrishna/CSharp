using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            StringDictionary sd = new StringDictionary();
            sd.Add("Key1", "KeyOne");
            sd.Add("Key2", "KeyTwo");
            sd.Remove("Key2");
            var value = sd["Key1"];
        }
    }
}
