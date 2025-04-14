namespace CodeInUnity.Core.Interfaces
{
  public interface ISeededRandom
  {
    int Seed { get; set; }

    double NextDouble();

    float NextFloat();

    float Next(float maxInclusive);

    int Next(int maxExclusive);

    void RandomizeTimeBased();

    float Range(float minInclusive, float maxInclusive);

    int Range(int minInclusive, int maxExclusive);

    void Reset();
  }
}