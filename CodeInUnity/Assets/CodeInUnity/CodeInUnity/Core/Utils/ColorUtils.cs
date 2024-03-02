using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class ColorUtils
  {
    private static Dictionary<Color, string> hexColorCache = new Dictionary<Color, string>();

    public static string ToRGBHex(Color c)
    {
      return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
    }

    public static string ToARGBHex(Color c)
    {
      return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a));
    }

    public static string ToHexWithCache(this Color color)
    {
      string result;

      if (!hexColorCache.TryGetValue(color, out result))
      {
        result =
            ((byte)(color.r * 255)).ToString("X2") +
            ((byte)(color.g * 255)).ToString("X2") +
            ((byte)(color.b * 255)).ToString("X2") +
            ((byte)(color.a * 255)).ToString("X2");

        hexColorCache[color] = result;
      }

      return result;
    }

    public static Vector4 ToVector4(Color c)
    {
      return new Vector4(c.r, c.g, c.b, c.a);
    }

    private static byte ToByte(float f)
    {
      f = Mathf.Clamp01(f);
      return (byte)(f * 255);
    }
  }
}
