using System;

namespace ExtensionMethods
{
    public static class ExtensionMethodsExample
    {
        public static bool IsGreaterthanZero(this int i)
        {
            return i > 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int age = 10;
            bool result = age.IsGreaterthanZero();
        }
    }
}
