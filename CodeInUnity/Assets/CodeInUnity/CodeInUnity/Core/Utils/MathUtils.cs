using System.Linq;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public static class MathUtils
  {
    public static int Fibbonacci(int number)
    {
      if (number == 0)
      {
        return 0;
      }

      return number + Fibbonacci(number - 1);
    }

    public static float Media(params float[] numbers)
    {
      if (numbers == null || numbers.Length == 0)
      {
        return 0;
      }

      return numbers.Sum() / numbers.Length;
    }

    public static float NormalizeAngle180(float angle)
    {
      while (angle > 180)
      {
        angle -= 360;
      }

      while (angle < -180)
      {
        angle += 360;
      }

      return angle;
    }

    public static float NormalizeAngle(float angle)
    {
      while (angle > 360)
      {
        angle -= 360;
      }

      while (angle < 0)
      {
        angle += 360;
      }

      return angle;
    }
    
    public static float WrapFloat(float value, float start, float end)
    {
      float width = end - start;   // 
      float offsetValue = value - start;   // value relative to 0

      return (offsetValue - (Mathf.Floor(offsetValue / width) * width)) + start;
    }
    
    public static int WrapInt(int value, int start, int end)
    {
      int width = end - start;   // 
      int offsetValue = value - start;   // value relative to 0

      return (offsetValue - ((offsetValue / width) * width)) + start;
    }
  }
}