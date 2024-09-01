using UnityEngine;

namespace CodeInUnity.Extensions
{
  public static class UnityVectorExtensions
  {
    public static Vector3 SetX(this Vector3 v, float x)
    {
      return new Vector3(x, v.y, v.z);
    }

    public static Vector3 SetY(this Vector3 v, float y)
    {
      return new Vector3(v.x, y, v.z);
    }

    public static Vector3 SetZ(this Vector3 v, float z)
    {
      return new Vector3(v.x, v.y, z);
    }

    public static Vector3 Add(this Vector3 a, Vector3 b)
    {
      return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3 Mult(this Vector3 a, Vector3 b)
    {
      return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector2 ToVector2(this Vector2Int v)
    {
      return new Vector2(v.x, v.y);
    }

    public static Vector2 Round(this Vector2 a)
    {
      return new Vector2(Mathf.Round(a.x), Mathf.Round(a.y));
    }

    public static Vector2Int RoundToInt(this Vector2 a)
    {
      return new Vector2Int(Mathf.RoundToInt(a.x), Mathf.RoundToInt(a.y));
    }

    public static Vector3 Round(this Vector3 a)
    {
      return new Vector3(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z));
    }
  }
}
