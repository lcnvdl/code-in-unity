using System;

namespace CodeInUnity.Core.Entities
{
  [Serializable]
  public struct MinMaxInt
  {
    public int min;

    public int max;

    public static MinMaxInt Empty => new MinMaxInt() { min = 0, max = 0 };
  }
}
