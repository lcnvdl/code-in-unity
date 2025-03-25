using UnityEngine;

namespace CodeInUnity.Extensions
{
  public static class RectExtensions
  {
    public static Rect Expand(this RectInt rect, float value)
    {
      return new Rect(rect.xMin - value, rect.yMin - value, rect.width + value * 2, rect.height + value * 2);
    }

    public static Rect Expand(this RectInt rect, Vector2 value)
    {
      return new Rect(rect.xMin - value.x, rect.yMin - value.y, rect.width + value.x * 2, rect.height + value.y * 2);
    }

    public static bool IsInBorder(this Rect rect, Vector2 point)
    {
      return point.x == rect.xMin || point.x == rect.xMax - 1 || point.y == rect.yMin || point.y == rect.yMax - 1;
    }

    public static RectInt RoundToInt(this Rect rect)
    {
      return new RectInt(rect.position.RoundToInt(), rect.size.RoundToInt());
    }
  }
}
