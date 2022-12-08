using UnityEngine;

namespace CodeInUnity.Utils
{
    public static class LayerMaskUtils
    {
        /// <summary>
        /// Extension method to check if a layer is in a layermask
        /// </summary>
        public static bool Contains(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
