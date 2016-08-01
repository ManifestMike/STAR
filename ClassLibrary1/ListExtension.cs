using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ListExtension
    {
       public static void MoveElementToIndex<T>(this List<T> list, int element, int index) {
            T item = list[element];
            list.RemoveAt(element);
            list.Insert(index, item);
        }
    }
}
