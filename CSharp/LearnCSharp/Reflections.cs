using System;
using System.Reflection;

namespace Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            //Viewing Type Information
            Type t = typeof(string);
            var constructors = t.GetConstructors();
            MemberInfo[] allMembers = t.GetMembers();
            MethodInfo[] methods = t.GetMethods(); //t.GetMethod("MethodName");
            FieldInfo[] fields = t.GetFields();
            PropertyInfo[] properties = t.GetProperties();

            //Get Custom Attributes
            var attributes = t.GetCustomAttributes();

            //Create instance 
            var instance = Activator.CreateInstance(t);
        }
    }
}
