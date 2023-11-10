using System;
using UnityEngine;
using S = System;

namespace CodeInUnity.Core.Security
{
  [S.Serializable]
  public class SeededRandom
  {
    [SerializeField]
    [HideInInspector]
    private int currentSeed = 1;

    public int Seed
    {
      get => this.currentSeed;
      set
      {
        this.randomInstance = null;
        this.currentSeed = value;
      }
    }

    private S.Random randomInstance = null;

    private S.Random RandomInstance => this.randomInstance ?? (this.randomInstance = new S.Random(this.currentSeed));

    public float Range(float minInclusive, float maxInclusive)
    {
      double next = this.RandomInstance.NextDouble();
      return minInclusive + (float)((maxInclusive - minInclusive) * next);
    }

    public double Next()
    {
      double next = this.RandomInstance.NextDouble();
      return next;
    }

    public float Next(float maxInclusive)
    {
      double next = this.RandomInstance.NextDouble();
      return (float)(maxInclusive * next);
    }

    public int Range(int minInclusive, int maxExclusive)
    {
      return this.RandomInstance.Next(minInclusive, maxExclusive);
    }

    public int Next(int maxExclusive)
    {
      return this.RandomInstance.Next(maxExclusive);
    }

    public void RandomizeTimeBased()
    {
      this.Seed = (int)(DateTime.Now.Ticks % 9999);
    }

    public static SeededRandom NewRandomized()
    {
      var random = new SeededRandom();
      random.RandomizeTimeBased();

      return random;
    }
  }
}
