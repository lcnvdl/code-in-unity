﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.CodeInUnity.CodeInUnity.Core.Utils
{
    public static class ReflectionUtils
    {
        // Deep clone
        public static T DeepClone<T>(T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
