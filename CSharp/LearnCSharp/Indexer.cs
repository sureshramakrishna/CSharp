using System;
using System.Collections.Generic;

namespace Indexer
{
    class SingelDimensionalArray
    {
        private readonly int[] collection = new int[10];
        public int this[int index]
        {
            get
            {
                if (index >= collection.Length)
                    throw new Exception("Out of range!");
                return collection[index];
            }
            set
            {
                if (index >= collection.Length)
                    throw new Exception("Out of range!");
                collection[index] = value;
            }
        }
    }
    class MultiDimensionalArray
    {
        private readonly SingelDimensionalArray[] collection = new SingelDimensionalArray[10];
        public SingelDimensionalArray this[int index]
        {
            get
            {
                if (index >= collection.Length)
                    throw new Exception("Out of range!");
                return collection[index];
            }
            set
            {
                if (index >= collection.Length)
                    throw new Exception("Out of range!");
                collection[index] = value;
            }
        }
    }
    class MultiDimensionalArray_2
    {
        private int[,] collection = new int[10, 10];
        public int this[int i, int j]
        {
            get
            {
                if (i >= 10 || j >= 10)
                    throw new Exception("Out of range!");
                return collection[i, j];
            }
            set
            {
                if (i >= 10 || j >= 10)
                    throw new Exception("Out of range!");
                collection[i, j] = value;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SingelDimensionalArray ca = new SingelDimensionalArray();
            ca[0] = 1;

            //You can use muli dimension by makeing this[int index] to this[int i1, int i2] and access using obj[0, 0]
            //If you want to use multi dimension similar to array, you need to make use of nested indexers like below.

            MultiDimensionalArray ma = new MultiDimensionalArray();
            ma[0] = new SingelDimensionalArray();
            ma[0][0] = 1;

            MultiDimensionalArray_2 ma_2 = new MultiDimensionalArray_2();
            ma_2[0, 0] = 0;
        }
    }
}
