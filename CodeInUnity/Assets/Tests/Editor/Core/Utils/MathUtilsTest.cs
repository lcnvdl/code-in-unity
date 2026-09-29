using CodeInUnity.Core.Utils;
using NUnit.Framework;

namespace Tests
{
  public class MathUtilsTest
  {
    [Test]
    public void Media_ShouldReturnZero_WhenNumbersIsNull()
    {
      float result = MathUtils.Media(null);
      Assert.AreEqual(0f, result);
    }

    [Test]
    public void Media_ShouldReturnZero_WhenNumbersIsEmpty()
    {
      float result = MathUtils.Media();
      Assert.AreEqual(0f, result);
    }

    [Test]
    public void Media_ShouldReturnAverage_OfGivenNumbers()
    {
      float result = MathUtils.Media(1f, 2f, 3f, 4f);
      Assert.AreEqual(2.5f, result);
    }

    [Test]
    public void Media_ShouldWorkFine_WithSingleNumber()
    {
      float result = MathUtils.Media(5f);
      Assert.AreEqual(5f, result);
    }
  }
}
