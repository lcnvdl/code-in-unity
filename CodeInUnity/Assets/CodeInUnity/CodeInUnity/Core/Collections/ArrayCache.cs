using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeInUnity.Core.Collections
{
  /// <summary>
  /// Cache to avoid using Linq .ToArray() method
  /// </summary>
  public class ArrayCache<T>
  {
    public bool shrinkAllowed = false;

    private T[] cache;

    public ArrayCache()
    {
    }

    public ArrayCache(int size)
    {
      cache = new T[size];
    }

    public ArrayCache<T> WithShrinkAllowed()
    {
      this.shrinkAllowed = true;
      return this;
    }

    public T[] ToArray(IEnumerable<T> list)
    {
      if (cache == null)
      {
        return cache = list.ToArray();
      }

      if (list is ICollection<T> collection)
      {
        if (cache.Length < collection.Count)
        {
          cache = list.ToArray();
        }
        else if (cache.Length == collection.Count)
        {
          collection.CopyTo(cache, 0);
        }
        else if (!this.shrinkAllowed)
        {
          collection.CopyTo(cache, 0);

          Array.Clear(cache, collection.Count, cache.Length - collection.Count);
        }
        else
        {
          cache = list.ToArray();
        }

        return cache;
      }
      else
      {
        int count = list.Count();

        if (cache.Length < count)
        {
          cache = list.ToArray();
        }
        else if (cache.Length == count)
        {
          for (int i = 0; i < count; i++)
          {
            cache[i] = list.ElementAt(i);
          }
        }
        else if (!this.shrinkAllowed)
        {
          Array.Clear(cache, count, cache.Length - count);

          for (int i = 0; i < count; i++)
          {
            cache[i] = list.ElementAt(i);
          }
        }
        else
        {
          cache = list.ToArray();
        }

        return cache;
      }
    }

    public T[] ToArray(List<T> list)
    {
      if (cache == null || cache.Length < list.Count)
      {
        cache = list.ToArray();
      }
      else if (cache.Length == list.Count)
      {
        list.CopyTo(cache);
      }
      else if (!this.shrinkAllowed)
      {
        Array.Clear(cache, list.Count, cache.Length - list.Count);
        list.CopyTo(0, cache, 0, list.Count);
      }
      else
      {
        cache = list.ToArray();
      }

      return cache;
    }
  }
}
