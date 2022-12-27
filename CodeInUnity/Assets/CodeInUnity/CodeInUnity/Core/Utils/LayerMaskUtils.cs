using UnityEngine;

namespace CodeInUnity.Utils
{
    public static class LayerMaskUtils
    {
        public const int Everything = -1;

        public const int Nothing = 0;

        public const int Default = 1;

        /// <summary>
        /// Checks if a layer is in a layermask
        /// </summary>
        public static bool Contains(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}
