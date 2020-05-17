using System.Collections.Generic;
using CodeInUnity.Core.Utils;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ConversorTest
    {
        [Test]
        public void NumberToK_100_Without_Decimals()
        {
            string expected = "100";
            string result = Conversor.NumberToK(100, 0);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberToK_1000_Without_Decimals()
        {
            string expected = "1K";
            string result = Conversor.NumberToK(1000, 0);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberToK_1100_Without_Decimals()
        {
            string expected = "1K";
            string result = Conversor.NumberToK(1100, 0);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberToK_1900_Without_Decimals()
        {
            string expected = "1K";
            string result = Conversor.NumberToK(1900, 0);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberToK_1100_With_Decimals()
        {
            string expected = "1.1K";
            string result = Conversor.NumberToK(1100, 1);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberToK_1100_With_2Decimals()
        {
            string expected = "1.1K";
            string result = Conversor.NumberToK(1100, 2);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NumberToK_1140_With_2Decimals()
        {
            string expected = "1.14K";
            string result = Conversor.NumberToK(1140, 2);

            Assert.AreEqual(expected, result);
        }
    }
}
