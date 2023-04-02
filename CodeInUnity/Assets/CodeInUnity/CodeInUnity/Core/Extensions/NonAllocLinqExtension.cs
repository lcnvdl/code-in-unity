using System.Collections.Generic;

namespace System.Linq
{
    public static class NonAllocLinqExtension
    {
        public static bool AnyNA<T>(this List<T> self)
        {
            return self.Count > 0;
        }

        public static void QuickSortNA<T>(this List<T> items, Func<T, T, int> compare)
        {
            QuickSortArray(items, compare, 0, items.Count - 1);
        }

        public static void SortNA<T>(this List<T> items, Func<T, T, int> compare)
        {
            int n = items.Count;

            for (int i = 1; i < n; ++i)
            {
                T key = items[i];
                int j = i - 1;

                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && compare(items[j], key) > 0)
                {
                    items[j + 1] = items[j];
                    j = j - 1;
                }

                items[j + 1] = key;
            }
        }

        public static bool AnyNA<T>(this T[] self)
        {
            return self.Length > 0;
        }

        private static void QuickSortArray<T>(this List<T> array, Func<T, T, int> compare, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];

            while (i <= j)
            {
                while (compare(array[i], pivot) < 0)
                {
                    i++;
                }

                while (compare(array[j], pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSortArray(array, compare, leftIndex, j);
            if (i < rightIndex)
                QuickSortArray(array, compare, i, rightIndex);
        }
    }
}
