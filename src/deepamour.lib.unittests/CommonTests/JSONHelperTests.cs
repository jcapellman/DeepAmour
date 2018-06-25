using System;
using System.Collections.Generic;
using System.Linq;

using deepamour.lib.Common;
using deepamour.lib.Predictors.Base;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace deepamour.lib.unittests.CommonTests
{
    [TestClass]
    public class JSONHelperTests
    {
        [TestMethod]
        public void JSONHelperTests_NullDeserializer()
        {
            var result = JSONHelper.DeserializeFromJson<string>(null);

            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(ArgumentNullException));
        }

        [TestMethod]
        public void JSONHelperTests_EmptyDeserializer()
        {
            var result = string.Empty.DeserializeFromJson<string>();

            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(ArgumentNullException));
        }

        [TestMethod]
        public void JSONHelperTests_BadJSONDeserializer()
        {
            var result = "{test:1234}".DeserializeFromJson<string>();

            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(JsonReaderException));
        }

        private class JsonDeserializerTest
        {
            public string name { get; set; }

            public string filename { get; set; }
        }

        [TestMethod]
        public void JSONHelperTests_GoodJSONDeserializer()
        {
            var result = @"
                          [
                            {
                              ""name"": ""Tears"",
                              ""filename"": ""Tears.mp3""
                            }
                          ]".DeserializeFromJson<List<JsonDeserializerTest>>();

            Assert.IsFalse(result.IsNullOrError);
            Assert.IsNotNull(result.Value);
            Assert.IsTrue(result.Value.Count == 1);
            Assert.IsNotNull(result.Value.FirstOrDefault(a => a.name == "Tears" && a.filename == "Tears.mp3"));

            var firstmatch = result.Value.FirstOrDefault();

            Assert.IsNotNull(firstmatch);
            Assert.AreEqual(firstmatch.filename, "Tears.mp3");
            Assert.AreEqual(firstmatch.name, "Tears");
        }

        private class JsonSerializeTest : BaseDataPrediction
        {
            public float Classification { get; set; }
        }

        [TestMethod]
        public void JSONHelperTests_NullSerializer()
        {
            var result = JSONHelper.SerializeFromJson<JsonDeserializerTest>(null);

            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(ArgumentNullException));
        }

        [TestMethod]
        public void JSONHelperTests_DefaultObjectSerializer()
        {
            var result = new JsonSerializeTest().SerializeFromJson<JsonDeserializerTest>();

            Assert.IsFalse(result.IsNullOrError);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("{\"Score\":0.0,\"Classification\":0.0}", result.Value);
        }

        [TestMethod]
        public void JSONHelperTests_InitializeObjectSerializer()
        {
            var test = new JsonSerializeTest
            {
                Classification = 1.0f,
                Score = 1.0f
            };

            var result = test.SerializeFromJson<JsonDeserializerTest>();

            Assert.IsFalse(result.IsNullOrError);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual("{\"Score\":1.0,\"Classification\":1.0}", result.Value);
        }
    }
}