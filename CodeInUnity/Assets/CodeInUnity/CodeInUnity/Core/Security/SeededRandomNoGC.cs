using System;
using CodeInUnity.Core.Interfaces;
using UnityEngine;

namespace CodeInUnity.Core.Security
{
  [Serializable]
  public class SeededRandomNoGC : ISeededRandom
  {
    [SerializeField]
    [HideInInspector]
    private uint seed = 1;

    [SerializeField]
    [HideInInspector]
    private uint state;

    public int Seed
    {
      get
      {
        return (int)this.seed;
      }

      set
      {
        this.SetSeed((uint)value);
      }
    }

    public SeededRandomNoGC()
    {
      this.state = 1u;
    }

    public SeededRandomNoGC(int seed)
    {
      this.Seed = seed;
    }

    public SeededRandomNoGC(uint seed)
    {
      this.SetSeed(seed);
    }

    public void RandomizeTimeBased()
    {
      this.SetSeed((uint)(DateTime.Now.Ticks % uint.MaxValue));
    }

    public void Reset()
    {
      this.state = this.seed;
    }

    public float NextFloat()
    {
      return (NextUInt() & 0xFFFFFF) / (float)0x1000000;
    }

    public float Range(float minInclusive, float maxInclusive)
    {
      return minInclusive + (maxInclusive - minInclusive) * NextFloat();
    }

    public int Range(int minInclusive, int maxExclusive)
    {
      return minInclusive + (int)(NextFloat() * (maxExclusive - minInclusive));
    }

    public int Next(int maxExclusive)
    {
      return (int)(NextFloat() * maxExclusive);
    }

    public float Next(float maxInclusive)
    {
      return NextFloat() * maxInclusive;
    }

    public SeededRandomNoGC GetNewWithSameSeed()
    {
      var copy = new SeededRandomNoGC(this.seed);
      return copy;
    }

    public static SeededRandomNoGC NewRandomized()
    {
      var r = new SeededRandomNoGC();
      r.RandomizeTimeBased();
      return r;
    }

    public double NextDouble()
    {
      return (double)NextFloat();
    }

    private uint NextUInt()
    {
      this.state ^= this.state << 13;
      this.state ^= this.state >> 17;
      this.state ^= this.state << 5;

      return this.state;
    }

    private void SetSeed(uint value)
    {
      this.seed = value == 0 ? 1u : value; // evitar seed 0
      this.state = this.seed;
    }
  }
}
