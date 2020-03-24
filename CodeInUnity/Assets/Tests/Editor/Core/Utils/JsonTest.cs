using System.Collections.Generic;
using CodeInUnity.Core.Utils;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class JsonTest
    {
        [Test]
        public void Deserialize()
        {
            var jsonString = "{ \"array\": [1.44,2,3], " +
                            "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
                            "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog\", " +
                            "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
                            "\"int\": 65536, " +
                            "\"float\": 3.1415926, " +
                            "\"bool\": true, " +
                            "\"null\": null }";

            var dict = JSON.Deserialize(jsonString) as Dictionary<string, object>;

            Assert.IsNotNull(dict);
            Assert.AreEqual(1.44, ((List<object>)dict["array"])[0]);
            Assert.AreEqual(2, ((List<object>)dict["array"])[1]);
            Assert.AreEqual(3, ((List<object>)dict["array"])[2]);
            Assert.AreEqual(3.1415926, dict["float"]);
            Assert.AreEqual(65536, dict["int"]);
            Assert.AreEqual(true, dict["bool"]);
            Assert.AreEqual(null, dict["null"]);
            Assert.AreEqual("The quick brown fox \"jumps\" over the lazy dog", dict["string"]);
        }
    }
}
