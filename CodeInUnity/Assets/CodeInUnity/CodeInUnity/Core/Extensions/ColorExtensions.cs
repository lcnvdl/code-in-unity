using UnityEngine;

namespace CodeInUnity.Extensions
{
  public static class ColorExtensions
  {
    public static Color SetAlpha(this Color c, float a)
    {
      return new Color(c.r, c.g, c.b, a);
    }
  }
}
