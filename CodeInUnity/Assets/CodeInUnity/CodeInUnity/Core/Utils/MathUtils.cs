using S = System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

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

    public static ulong SumUlong(params ulong[] numbers)
    {
      if (numbers == null || numbers.Length == 0)
      {
        return 0;
      }

      ulong sum = 0;

      foreach (var value in numbers)
      {
        sum += value;
      }

      return sum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPair(int x)
    {
      return x % 2 == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Mod(int x, int m)
    {
      return (x % m + m) % m;
    }

    public static float Mod(float x, float m)
    {
      return x - m * Mathf.Floor(x / m);
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

    public static bool IsSimilar(float a, float b, float delta = float.Epsilon)
    {
      return Mathf.Abs(a - b) <= delta;
    }

    public static bool IsBetween(float value, float min, float max)
    {
      return value >= min && value <= max;
    }

    public static float Round(float value, int digits)
    {
      return (float)S.Math.Round(value, digits);
    }

    public static int IncreaseInRange(ref int index, int maxValue)
    {
      index = Mod(index + 1, maxValue);
      return index;
    }
  }
}