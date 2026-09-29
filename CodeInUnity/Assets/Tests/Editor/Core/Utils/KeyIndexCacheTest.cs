using CodeInUnity.Core.Utils;
using NUnit.Framework;

namespace Tests
{
  public class KeyIndexCacheTest
  {
    private KeyIndexCache cache;

    [SetUp]
    public void SetUp()
    {
      this.cache = new KeyIndexCache();
    }

    [Test]
    public void Set_ShouldPersistValue_WhenKeyDoesNotExistYet()
    {
      this.cache.Set("key", 0, "value");

      Assert.IsTrue(this.cache.Exists("key", 0));
      Assert.AreEqual("value", this.cache.Get("key", 0));
    }

    [Test]
    public void Set_ShouldPersistValue_WhenKeyAlreadyExists()
    {
      this.cache.Set("key", 0, "first");
      this.cache.Set("key", 1, "second");

      Assert.AreEqual("first", this.cache.Get("key", 0));
      Assert.AreEqual("second", this.cache.Get("key", 1));
    }

    [Test]
    public void Set_ShouldOverwriteExistingValue_ForSameKeyAndIndex()
    {
      this.cache.Set("key", 0, "first");
      this.cache.Set("key", 0, "second");

      Assert.AreEqual("second", this.cache.Get("key", 0));
    }

    [Test]
    public void Exists_ShouldReturnFalse_WhenValueWasNeverSet()
    {
      Assert.IsFalse(this.cache.Exists("key", 0));
    }

    [Test]
    public void Get_ShouldReturnNull_WhenValueWasNeverSet()
    {
      Assert.IsNull(this.cache.Get("key", 0));
    }
  }
}
