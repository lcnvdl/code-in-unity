using System.Collections.Generic;

namespace CodeInUnity.Core.Utils
{
  public class FixedListsCache<T>
  {
    private const int DEFAULT_MIN_CAPACITY = 2;
    
    private const int DEFAULT_MAX_CAPACITY = 100;

    private static FixedListsCache<T> globalInstance;

    public static FixedListsCache<T> GlobalInstance
    {
      get
      {
        if (globalInstance == null)
        {
          globalInstance = new FixedListsCache<T>();
        }

        return globalInstance;
      }
    }

    public static FixedListsCache<T> RawGlobalInstance => globalInstance;

    private readonly List<T>[] lists;

    private readonly int minCapacity;

    public FixedListsCache(int minCapacity = DEFAULT_MIN_CAPACITY, int maxCapacity = DEFAULT_MAX_CAPACITY)
    {
      this.minCapacity = minCapacity;
      this.lists = new List<T>[maxCapacity - minCapacity + 1];
    }

    public List<T> Get(int capacity)
    {
      int index = capacity - this.minCapacity;
      List<T> list = this.lists[index];

      if (list == null)
      {
        list = new List<T>(capacity);
        this.lists[index] = list;
      }
      else if (list.Count > 0)
      {
        list.Clear();
      }

      return list;
    }

    public List<T> Get(int capacity, IEnumerable<T> initializer)
    {
      List<T> list = this.Get(capacity);

      foreach (T item in initializer)
      {
        list.Add(item);
      }

      return list;
    }

    public List<T> Get(int capacity, List<T> initializer)
    {
      List<T> list = this.Get(capacity);

      for (int i = 0; i < initializer.Count && i < capacity; i++)
      {
        list.Add(initializer[i]);
      }

      return list;
    }
  }
}
