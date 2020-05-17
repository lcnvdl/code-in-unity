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
    }
}
