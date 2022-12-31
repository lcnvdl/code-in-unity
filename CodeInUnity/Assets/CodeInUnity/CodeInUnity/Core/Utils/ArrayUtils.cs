using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.Utils
{
    public class ArrayUtils
    {
        public static T Choose<T>(params T[] array)
        {
            if (array.Length == 0)
                return default(T);
            else if (array.Length == 1)
                return array[0];
            else
                return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Choose<T>(List<T> array)
        {
            if (array.Count == 0)
                return default(T);
            else
                return array[UnityEngine.Random.Range(0, array.Count)];
        }
    }
}
