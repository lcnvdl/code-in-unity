using CodeInUnity.Core.System;
using NUnit.Framework;

namespace Tests
{
    public class PromiseTest
    {
        [Test]
        public void Promise_Then()
        {
            Promise<int> promise = new Promise<int>((resolve, reject) =>
            {
                resolve(1);
            });

            int value = 0;

            promise.Then(m => value = m);

            Assert.AreEqual(1, value);
        }
    }
}
