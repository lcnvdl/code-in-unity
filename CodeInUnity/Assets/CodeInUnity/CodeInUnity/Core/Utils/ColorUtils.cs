using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class ColorUtils
  {
    public static string ToRGBHex(Color c)
    {
      return string.Format("#{0:X2}{1:X2}{2:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b));
    }

    public static string ToARGBHex(Color c)
    {
      return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a));
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
