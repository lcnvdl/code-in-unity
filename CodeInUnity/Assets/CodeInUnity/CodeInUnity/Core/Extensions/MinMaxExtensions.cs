using CodeInUnity.Core.Security;

namespace CodeInUnity.Core.Entities
{
  public static class MinMaxExtensions
  {
    public static int Random(this MinMaxInt minMax)
    {
      return UnityEngine.Random.Range(minMax.min, minMax.max + 1);
    }

    public static int Random(this MinMaxInt minMax, SeededRandom random)
    {
      return random.Range(minMax.min, minMax.max + 1);
    }

    public static float Random(this MinMax minMax)
    {
      return UnityEngine.Random.Range(minMax.min, minMax.max);
    }

    public static float Random(this MinMax minMax, SeededRandom random)
    {
      return random.Range(minMax.min, minMax.max);
    }
  }
}
