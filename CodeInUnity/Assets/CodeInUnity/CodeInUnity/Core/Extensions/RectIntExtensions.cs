using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.Extensions
{
  public static class RectIntExtensions
  {
    public static RectInt Expand(this RectInt rect, int value)
    {
      return new RectInt(rect.xMin - value, rect.yMin - value, rect.width + value * 2, rect.height + value * 2);
    }

    public static RectInt Expand(this RectInt rect, Vector2Int value)
    {
      return new RectInt(rect.xMin - value.x, rect.yMin - value.y, rect.width + value.x * 2, rect.height + value.y * 2);
    }

    public static IEnumerable<Vector2Int> GetAllPoints(this RectInt rect)
    {
      for (int x = rect.xMin; x < rect.xMax; x++)
      {
        for (int y = rect.yMin; y < rect.yMax; y++)
        {
          yield return new Vector2Int(x, y);
        }
      }
    }

    public static bool IsPointInBorder(this RectInt rect, Vector2Int point)
    {
      return point.x == rect.xMin || point.x == rect.xMax - 1 || point.y == rect.yMin || point.y == rect.yMax - 1;
    }
  }
}
