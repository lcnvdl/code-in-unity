using System.Collections.Generic;

namespace System.Linq
{
  public static class NonAllocLinqExtension
  {
    public static void ExceptTo<T>(this List<T> self, List<T> exceptList, List<T> to)
    {
      foreach (T entity in self)
      {
        if (!exceptList.Contains(entity))
        {
          to.Add(entity);
        }
      }
    }

    public static T FirstOrDefaultNA<T>(this List<T> self)
    {
      return self.Count > 0 ? self[0] : default(T);
    }

    public static T ShiftNA<T>(this List<T> self)
    {
      if (self.Count == 0)
      {
        return default(T);
      }

      var instance = self[0];
      self.RemoveAt(0);
      return instance;
    }

    public static T PopNA<T>(this List<T> self)
    {
      if (self.Count == 0)
      {
        return default(T);
      }

      var instance = self[self.Count - 1];
      self.RemoveAt(self.Count - 1);
      return instance;
    }

    public static T LastOrDefaultNA<T>(this List<T> self)
    {
      return self.Count > 0 ? self[self.Count - 1] : default(T);
    }

    public static T LastNA<T>(this List<T> self)
    {
      return self[self.Count - 1];
    }

    public static bool AnyNA<T>(this List<T> self)
    {
      return self.Count > 0;
    }

    public static void QuickSortNA<T>(this List<T> items, Func<T, T, int> compare)
    {
      QuickSortArray(items, compare, 0, items.Count - 1);
    }

    public static bool ContainsNA<T>(this List<T> self, T value)
    {
      int count = self.Count;
      for (int i = 0; i < count; ++i)
      {
        if (SafeEquals(self[i], value))
        {
          return true;
        }
      }

      return false;
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

    public static bool ContainsNA<T>(this T[] self, T value)
    {
      int count = self.Length;
      for (int i = 0; i < count; ++i)
      {
        if (SafeEquals(self[i], value))
        {
          return true;
        }
      }

      return false;
    }

    public static bool ContainsOrdinal(this List<string> self, string value)
    {
      int count = self.Count;
      for (int i = 0; i < count; ++i)
      {
        if (value.Equals(self[i], StringComparison.Ordinal))
        {
          return true;
        }
      }

      return false;
    }

    public static bool ContainsOrdinal(this string[] self, string value)
    {
      int count = self.Length;
      for (int i = 0; i < count; ++i)
      {
        if (value.Equals(self[i], StringComparison.Ordinal))
        {
          return true;
        }
      }

      return false;
    }

    private static bool SafeEquals(object a, object b)
    {
      if (a != null)
      {
        return a.Equals(b);
      }

      if (b != null)
      {
        return b.Equals(a);
      }

      return a == b;
    }

    private static void QuickSortArray<T>(this List<T> array, Func<T, T, int> compare, int leftIndex, int rightIndex)
    {
      int i = leftIndex;
      int j = rightIndex;
      T pivot = array[leftIndex];

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
      {
        QuickSortArray(array, compare, leftIndex, j);
      }

      if (i < rightIndex)
      {
        QuickSortArray(array, compare, i, rightIndex);
      }
    }
  }
}
