using CodeInUnity.Core.Security;
using NUnit.Framework;

public class SeededRandomNoGCTestTest
{
  [Test]
  public void SeededRandomNoGC_ShouldWorkFine()
  {
    var sr = new SeededRandomNoGC(1);

    Assert.AreEqual(0.0161152482f, sr.NextFloat());
    Assert.AreEqual(0.0313416123f, sr.NextFloat());
    Assert.AreEqual(0.799450219f, sr.NextFloat());
    Assert.AreEqual(0.334370553f, sr.NextFloat());
    Assert.AreEqual(0.97301966f, sr.NextFloat());

    sr = new SeededRandomNoGC(1);

    Assert.AreEqual(0.0161152482f, sr.NextFloat());

    sr = new SeededRandomNoGC(2);

    Assert.AreNotEqual(0.0161152482f, sr.NextFloat());
  }
}
