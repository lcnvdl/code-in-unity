using System.Collections.Generic;

namespace CodeInUnity.Core.Utils
{
    public static class ArrayUtils
    {
        public static T Choose<T>(params T[] array)
        {
            if (array.Length == 0)
                return default;
            else if (array.Length == 1)
                return array[0];
            else
                return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Choose<T>(List<T> array)
        {
            if (array.Count == 0)
                return default;
            else
                return array[UnityEngine.Random.Range(0, array.Count)];
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
