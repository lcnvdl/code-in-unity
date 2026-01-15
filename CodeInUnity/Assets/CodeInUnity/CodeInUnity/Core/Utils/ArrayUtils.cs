using System.Collections.Generic;

namespace CodeInUnity.Core.Utils
{
  public static class ArrayUtils
  {
    public static T Choose<T>(params T[] array)
    {
      switch (array.Length)
      {
        case 0:
          return default;

        case 1:
          return array[0];

        default:
          return array[UnityEngine.Random.Range(0, array.Length)];
      }
    }

    public static T Choose<T>(List<T> array)
    {
      switch (array.Count)
      {
        case 0:
          return default;

        case 1:
          return array[0];

        default:
          return array[UnityEngine.Random.Range(0, array.Count)];
      }
    }

    public static T PickFirstNotNull<T>(T instance1, T instance2)
    {
      if (instance1 != null)
      {
        return instance1;
      }

      if (instance2 != null)
      {
        return instance2;
      }

      return default;
    }

    public static T PickFirstNotNull<T>(params T[] instances)
    {
      int length = instances.Length;

      for (int i = 0; i < length; i++)
      {
        if (instances[i] != null)
        {
          return instances[i];
        }
      }

      return default;
    }
  }
}
