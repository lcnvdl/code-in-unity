using CodeInUnity.Extensions;
using CodeInUnity.Scripts.GameObjects;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
  public class MonoBehaviourExtensionsTest
  {
    private GameObject gameObject;

    [SetUp]
    public void SetUp()
    {
      this.gameObject = new GameObject("MonoBehaviourExtensionsTest");
    }

    [TearDown]
    public void TearDown()
    {
      Object.DestroyImmediate(this.gameObject);
    }

    [Test]
    public void HasCustomTag_ShouldReturnTrue_WhenTagExists()
    {
      CustomTagScript.AddTag(this.gameObject, "enemy");

      Assert.IsTrue(this.gameObject.transform.HasCustomTag("enemy"));
    }

    [Test]
    public void HasCustomTag_ShouldReturnFalse_WhenTagDoesNotExist()
    {
      CustomTagScript.AddTag(this.gameObject, "enemy");

      Assert.IsFalse(this.gameObject.transform.HasCustomTag("ally"));
    }

    [Test]
    public void HasCustomTag_ShouldMatchByAdditionalValue_WhenProvided()
    {
      CustomTagScript.AddTag(this.gameObject, "enemy", "boss");

      Assert.IsTrue(this.gameObject.transform.HasCustomTag("enemy", "boss"));
      Assert.IsFalse(this.gameObject.transform.HasCustomTag("enemy", "minion"));
    }

    [Test]
    public void GetCustomTag_ShouldReturnMatchingComponent()
    {
      var tag = CustomTagScript.AddTag(this.gameObject, "enemy");

      Assert.AreSame(tag, this.gameObject.transform.GetCustomTag("enemy"));
    }

    [Test]
    public void GetCustomTag_ShouldReturnNull_WhenNoTagMatches()
    {
      CustomTagScript.AddTag(this.gameObject, "enemy");

      Assert.IsNull(this.gameObject.transform.GetCustomTag("ally"));
    }

    [Test]
    public void GetCustomTag_ShouldReturnFirstMatch_WhenMultipleTagsArePresent()
    {
      CustomTagScript.AddTag(this.gameObject, "ally");
      var enemyTag = CustomTagScript.AddTag(this.gameObject, "enemy");

      Assert.AreSame(enemyTag, this.gameObject.transform.GetCustomTag("enemy"));
    }
  }
}
