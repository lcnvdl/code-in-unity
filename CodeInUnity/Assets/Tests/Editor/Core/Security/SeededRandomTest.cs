using System;
using CodeInUnity.Core.Security;
using NUnit.Framework;

public class SeededRandomTest
{
  [Test]
  public void SeededRandom_ShouldWorkFine()
  {
    var sr = new SeededRandom(1);

    Assert.AreEqual(249, (int)Math.Round(sr.Next() * 1000));
    Assert.AreEqual(111, (int)Math.Round(sr.Next() * 1000));
    Assert.AreEqual(467, (int)Math.Round(sr.Next() * 1000));
    Assert.AreEqual(772, (int)Math.Round(sr.Next() * 1000));
    Assert.AreEqual(658, (int)Math.Round(sr.Next() * 1000));

    sr = new SeededRandom(1);

    Assert.AreEqual(249, (int)Math.Round(sr.Next() * 1000));

    sr = new SeededRandom(2);

    Assert.AreNotEqual(249, (int)Math.Round(sr.Next() * 1000));
  }
}
