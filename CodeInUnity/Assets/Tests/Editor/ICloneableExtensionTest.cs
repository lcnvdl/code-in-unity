using CodeInUnity.Extensions;
using CodeInUnity.Interfaces;
using NUnit.Framework;

namespace CodeInUnity.Tests
{
  public class MockCloneable : ICloneable<MockCloneable>
  {
    public int Value { get; set; }

    public MockCloneable()
    {
    }

    public MockCloneable(int value)
    {
      this.Value = value;
    }

    public MockCloneable Clone()
    {
      return new MockCloneable(this.Value);
    }
  }

  public class ICloneableExtensionsTests
  {
    [Test]
    public void CloneArrayOfCloneables_WithNullArray_ReturnsEmptyArray()
    {
      MockCloneable[] input = null;
      var result = new MockCloneable().CloneArrayOfCloneables(input);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    [Test]
    public void CloneArrayOfCloneables_WithEmptyArray_ReturnsEmptyArray()
    {
      MockCloneable[] input = new MockCloneable[0];
      var result = new MockCloneable().CloneArrayOfCloneables(input);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    [Test]
    public void CloneArrayOfCloneables_WithValidArray_ReturnsClonedArray()
    {
      var input = new MockCloneable[]
      {
        new MockCloneable(1),
        new MockCloneable(2),
        new MockCloneable(3)
      };

      var result = new MockCloneable().CloneArrayOfCloneables(input);

      Assert.IsNotNull(result);
      Assert.AreEqual(input.Length, result.Length);

      for (int i = 0; i < input.Length; i++)
      {
        Assert.IsNotNull(result[i]);
        Assert.AreEqual(input[i].Value, result[i].Value);
        Assert.AreNotSame(input[i], result[i]);
      }
    }
  }
}
