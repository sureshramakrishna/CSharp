using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class DeveloperAttribute : Attribute
    {
        private string name;
        private string level;

        public DeveloperAttribute(string name, string level)
        {
            this.name = name;
            this.level = level;
        }
        public virtual string Level { get => level; }
        public virtual string Name { get => name; }
    }
    [Developer("Class Level","1")]
    public class Usage
    {
        [Developer("Method Level", "2")]
        public void GetUsage()
        {

        }
    }

    public class Program
    {
        public static void Main()
        {
            Usage u = new Usage();
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(u.GetType());
            //or
            DeveloperAttribute da = (DeveloperAttribute)Attribute.GetCustomAttribute(u.GetType(), typeof(DeveloperAttribute));

            MemberInfo[] MyMemberInfo = u.GetType().GetMethods();
            for (int i = 0; i < MyMemberInfo.Length; i++)
            {
                da = (DeveloperAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DeveloperAttribute));
                if (da == null)
                    Console.WriteLine("No attribute in member function {0}.\n", MyMemberInfo[i].ToString());
                else
                    Console.WriteLine("{0} : Name, Level: {1}.", da.Name, da.Level);
            }
        }
    }
}
