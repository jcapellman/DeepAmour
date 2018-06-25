using System.Collections.Generic;

using deepamour.lib.cli.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.cli.unittests.Common
{
    [TestClass]
    public class CommandLineParserTests
    {
        [TestMethod]
        public void CLP_NullParameter()
        {
            var result = CommandLineParser.ParseArguments(null);
            
            Assert.IsTrue(result.IsNullOrError);
        }

        [TestMethod]
        public void CLP_EmptyParameter()
        {
            var result = CommandLineParser.ParseArguments(new List<string>());

            Assert.IsFalse(result.IsNullOrError);

            var defaultConstructor = new CommandLineParser.CommandLineArguments();

            Assert.AreEqual(result.Value.PredictionDataFileName, defaultConstructor.PredictionDataFileName);
            Assert.AreEqual(result.Value.Evaluate, defaultConstructor.Evaluate);
            Assert.AreEqual(result.Value.Predictor, defaultConstructor.Predictor);
            Assert.AreEqual(result.Value.TrainingDataFileName, defaultConstructor.TrainingDataFileName);
        }

        [TestMethod]
        public void CLP_InitializedParameter()
        {
            var args = new List<string>
            {
                "-pr",
                "warriors",
                "-pd",
                "test.txt",
                "-td",
                "test.txt",
                "-e"
            };

            var result = CommandLineParser.ParseArguments(args);

            Assert.IsFalse(result.IsNullOrError);
            
            Assert.IsTrue(result.Value.Evaluate);
        }
    }
}