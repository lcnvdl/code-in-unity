using System;
using CodeInUnity.Core.Interfaces;
using UnityEngine;
using S = System;

namespace CodeInUnity.Core.Security
{
  [S.Serializable]
  public class SeededRandom : ISeededRandom
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

    public SeededRandom()
    {
    }

    public SeededRandom(int seed)
    {
      this.Seed = seed;
    }

    public float Range(float minInclusive, float maxInclusive)
    {
      double next = this.RandomInstance.NextDouble();
      return minInclusive + (float)((maxInclusive - minInclusive) * next);
    }

    public double Next()
    {
      return this.RandomInstance.NextDouble();
    }

    public double NextDouble()
    {
      return this.RandomInstance.NextDouble();
    }

    public float NextFloat()
    {
      return (float)this.RandomInstance.NextDouble();
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

    public SeededRandom GetNewWithSameSeed()
    {
      return new SeededRandom(this.Seed);
    }

    public static SeededRandom NewRandomized()
    {
      var random = new SeededRandom();
      random.RandomizeTimeBased();

      return random;
    }

    public void Reset()
    {
      this.Seed = this.currentSeed;
    }
  }
}
