using System;

namespace CodeInUnity.Core.Entities
{
  [Serializable]
  public struct MinMax
  {
    public float min;

    public float max;

    public static MinMax Empty => new MinMax() { min = 0, max = 0 };
  }
}
