using System.Collections.Generic;



namespace Extensions {
    static class ListExtensions {
        static void MoveToFront<T>(this List<T> list, int index) {
            T item = list[index];
            for (int i = index; i > 0; i--)
                list[i] = list[i - 1];
            list[0] = item;
        }
    }
}