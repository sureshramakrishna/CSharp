using System;

namespace OperatorOverloading
{
    class Box
    {
        public double length { get; set; }
        public void Area()
        {
            Console.WriteLine(length * length);
        }
        public static Box operator +(Box b, Box c) //One of the parameters must be containing type.
        {
            Box box = new Box();
            box.length = b.length + c.length;
            return box;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Box box1 = new Box { length = 6.0 };   
            Box box2 = new Box { length = 7.0 }; 
            box1.Area();
            box2.Area();
            Box box3 = box1 + box2;
            box3.Area();
        }
    }
}
