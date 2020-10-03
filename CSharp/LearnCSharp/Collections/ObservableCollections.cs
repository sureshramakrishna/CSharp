using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<int> oc = new ObservableCollection<int>();
            oc.CollectionChanged += Oc_CollectionChanged;
            oc.Add(1);
            oc.Remove(2); //Does not throw error.
        }

        private static void Oc_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
