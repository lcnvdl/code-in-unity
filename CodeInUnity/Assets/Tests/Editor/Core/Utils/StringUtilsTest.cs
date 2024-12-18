using System.Collections.Generic;
using CodeInUnity.Core.Utils;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
  public class StringUtilsTest
  {
    [Test]
    public void FastEndsWith_True()
    {
      bool result = StringUtils.FastEndsWith("This is a text", "text");
      Assert.IsTrue(result);
    }

    [Test]
    public void FastEndsWith_False()
    {
      bool result = StringUtils.FastEndsWith("This is a text", "test");
      Assert.IsFalse(result);
    }

    [Test]
    public void CountOccurences_ShouldWorkFine()
    {
      int result = StringUtils.CountOccurences("this is a text", 't');
      Assert.AreEqual(3, result);
    }

    [Test]
    public void CountOccurences_MustNotIgnoreCase()
    {
      int result = StringUtils.CountOccurences("This is a text", 't');
      Assert.AreEqual(2, result);
    }

    [Test]
    public void CountOccurences_ShouldReturnZero_IfStringIsEmpty()
    {
      int result = StringUtils.CountOccurences("", 't');
      Assert.AreEqual(0, result);
    }
  }
}
