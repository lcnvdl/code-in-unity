using System;

namespace CodeInUnity.Core.Utils
{
  public class FixedArraysCache<T>
  {
    private const int DEFAULT_MIN_CAPACITY = 2;

    private const int DEFAULT_MAX_CAPACITY = 100;

    private static FixedArraysCache<T> globalInstance;

    public static FixedArraysCache<T> GlobalInstance
    {
      get
      {
        if (globalInstance == null)
        {
          globalInstance = new FixedArraysCache<T>();
        }

        return globalInstance;
      }
    }

    public static FixedArraysCache<T> RawGlobalInstance => globalInstance;

    private readonly T[][] arrays;

    private readonly int minCapacity;

    public FixedArraysCache(int minCapacity = DEFAULT_MIN_CAPACITY, int maxCapacity = DEFAULT_MAX_CAPACITY)
    {
      this.minCapacity = minCapacity;
      this.arrays = new T[maxCapacity - minCapacity + 1][];
    }

    public T[] Get(int size)
    {
      int index = size - this.minCapacity;
      T[] array = this.arrays[index];

      if (array == null)
      {
        array = new T[size];
        this.arrays[index] = array;
      }
      else
      {
        Array.Clear(array, 0, array.Length);
      }

      return array;
    }
  }
}
