using CodeInUnity.Core.Security;
using NUnit.Framework;

public class SeededRandomNoGCTestTest
{
  [Test]
  public void SeededRandomNoGC_ShouldWorkFine()
  {
    var sr = new SeededRandomNoGC(1);

    Assert.AreEqual(0.0161152482f, sr.Next());
    Assert.AreEqual(0.0313416123f, sr.Next());
    Assert.AreEqual(0.799450219f, sr.Next());
    Assert.AreEqual(0.334370553f, sr.Next());
    Assert.AreEqual(0.97301966f, sr.Next());

    sr = new SeededRandomNoGC(1);

    Assert.AreEqual(0.0161152482f, sr.Next());

    sr = new SeededRandomNoGC(2);

    Assert.AreNotEqual(0.0161152482f, sr.Next());
  }
}
