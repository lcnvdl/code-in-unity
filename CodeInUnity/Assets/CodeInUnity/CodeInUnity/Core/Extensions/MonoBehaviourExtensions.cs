using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeInUnity.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static T LazyGetInstance<T>(this MonoBehaviour self, ref T value, bool lookInChildren = true) where T : class
        {
            if (value == null)
            {
                if (!self.TryGetComponent<T>(out value) && lookInChildren)
                {
                    value = self.GetComponentInChildren<T>();
                }
            }

            return value;
        }
    }
}
