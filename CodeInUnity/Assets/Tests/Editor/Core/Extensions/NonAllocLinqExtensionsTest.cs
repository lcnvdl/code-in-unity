using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    public class NonAllocLinqExtensionsTest
    {
        private static int[] testArray;

        public NonAllocLinqExtensionsTest()
        {
            if (testArray == null)
            {
                testArray = new int[500];

                Random randNum = new Random(1);
                for (int i = 0; i < testArray.Length; i++)
                {
                    testArray[i] = randNum.Next(0, 100);
                }
            }
        }

        [Test]
        public void NonAllocLinqExtensions_QuickSort_ShouldWorkFine()
        {
            var list = testArray.ToList();
            list.QuickSortNA((a, b) => a - b);
            for (int i = 1; i < list.Count; i++)
            {
                Assert.IsTrue(list[i - 1] <= list[i]);
            }
        }

        [Test]
        public void NonAllocLinqExtensions_Sort_ShouldWorkFine()
        {
            var list = testArray.ToList();
            list.SortNA((a, b) => a - b);
            for (int i = 1; i < list.Count; i++)
            {
                Assert.IsTrue(list[i - 1] <= list[i]);
            }
        }
    }
}