using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ListExtension
    {
       public static void MoveToFront<T>(this List<T> list, int index) {
            T item = list[index];
            list.RemoveAt(index);
            list.Insert(0, item);
        }
    }
}
