using System;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a1 = new int[10];
            int[,] a2 = new int[10, 5];
            int[,,] a3 = new int[10, 5, 2];
            int[][] x = new int[2][]; //create variable size 2d array, also known as jagged Array. A jagged array is an array of an array.
            x[0] = new int[1];
            x[1] = new int[2];

            Array arr = Array.CreateInstance(typeof(int), 2, 3); //creates 2-dimensional array. Array.CreateInstance(typeof(int), 2, 3,4); creates 3-dimensional array.
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 3; j++)
                    arr.SetValue(1, i, j);
            int[,] array = (int[,])arr;
        }
    }
}
