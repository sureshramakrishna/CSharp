using System.Collections;

namespace ArrayLists
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList arrList = new ArrayList();
            arrList.Add(1);
            arrList.Add("String");
            arrList[1] = 2; //Indexing works only for items already added, cannot access 4th index if only 3 items are added.
            int count = arrList.Count; //number of elements actually in array
            arrList.Capacity = 10;// gets or sets the size of array
            arrList.Insert(2, 5); // index should be <= to count (not capacit), cannot give random index.
        }
    }
}
