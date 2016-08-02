using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions {
    public static class ListExtension {
        public static void MoveElementToIndex<T>(this List<T> list, int element, int index) {
            T item = list[element];
            list.RemoveAt(element);
            list.Insert(index, item);
        }

        public static string Delimit<T>(this IEnumerable<T> list, string delimiter = ", ") {
            return list
                .Aggregate(string.Empty, (output, s) => string.IsNullOrWhiteSpace(output) ? s.ToString() : $"{output}{delimiter} {s}");
        }
    }
}
