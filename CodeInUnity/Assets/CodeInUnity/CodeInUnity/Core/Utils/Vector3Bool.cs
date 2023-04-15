using System;

namespace CodeInUnity.Core.Utils
{
    [Serializable]
    public struct Vector3Bool
    {
        public bool x;

        public bool y;

        public bool z;

        public static readonly Vector3Bool True = new Vector3Bool() { x = true, y = true, z = true };

        public static readonly Vector3Bool False = new Vector3Bool() { x = false, y = false, z = false };
    }
}
