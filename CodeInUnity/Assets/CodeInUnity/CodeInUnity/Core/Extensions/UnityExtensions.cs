using CodeInUnity.Utils;
using UnityEngine;

namespace CodeInUnity.Extensions
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Extension method to check if a layer is in a layermask
        /// </summary>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return LayerMaskUtils.Contains(mask, layer);
        }
    }
}
