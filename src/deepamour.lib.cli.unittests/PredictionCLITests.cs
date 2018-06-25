using System.Collections.Generic;

using deepamour.lib.cli.Common;
using deepamour.lib.core.Predictors.WarriorsPredictor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.cli.unittests
{
    [TestClass]
    public class PredictionCLITests
    {
        [TestMethod]
        public void NullArgumentsTest()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var argResult = predictionCLI.LoadArguments(null);

            Assert.IsFalse(argResult);
        }

        [TestMethod]
        public void EmptyArgumentsTest()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var argResult = predictionCLI.LoadArguments(new string[] { });

            Assert.IsFalse(argResult);
        }

        [TestMethod]
        public void InitializedArgumentsValidTest()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

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

            var argResult = predictionCLI.LoadArguments(args.ToArray());

            Assert.IsTrue(argResult);
        }

        [TestMethod]
        public void InitializedArgumentsInvalidPredictorTest()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "test.txt",
                "-td",
                "test.txt"
            };

            var argResult = predictionCLI.LoadArguments(args.ToArray());

            Assert.IsFalse(argResult);
        }

        [TestMethod]
        public void InitializedArgumentsInvalidEvaluateTest()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-e"
            };

            var argResult = predictionCLI.LoadArguments(args.ToArray());

            Assert.IsFalse(argResult);
        }

        [TestMethod]
        public void InitializedArgumentsInvalidEvaluateFromPDTest()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "test.txt"
            };

            var argResult = predictionCLI.LoadArguments(args.ToArray());

            Assert.IsFalse(argResult);
        }

        [TestMethod]
        public void ValidateCL_Null()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var result = predictionCLI.ValidateCommandLine(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateCL_Default()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var result = predictionCLI.ValidateCommandLine(new CommandLineParser.CommandLineArguments());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateCL_InitializedFully()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new CommandLineParser.CommandLineArguments();

            args.PredictionDataFileName = "test.txt";
            args.Evaluate = true;
            args.Predictor = "Warriors";
            args.TrainingDataFileName = "test.txt";
            
            var result = predictionCLI.ValidateCommandLine(args);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateCL_InitializedEvaluateInvalid()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new CommandLineParser.CommandLineArguments();

            args.Evaluate = true;
            args.PredictionDataFileName = null;

            var result = predictionCLI.ValidateCommandLine(args);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateCL_InitializedPredictionInvalid()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new CommandLineParser.CommandLineArguments();

            args.PredictionDataFileName = "test.txt";
            args.Predictor = null;

            var result = predictionCLI.ValidateCommandLine(args);

            Assert.IsFalse(result);

            args.Predictor = "warrior";
            args.PredictionDataFileName = null;

            result = predictionCLI.ValidateCommandLine(args);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RunPrediction_Null()
        {
            var predictionCLI = new PredictionCLI();
            
            Assert.IsNotNull(predictionCLI);

            predictionCLI.LoadArguments(null);

            predictionCLI.RunPrediction();
        }

        [TestMethod]
        public void RunPrediction_InitProperlyPrediction()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "test.txt",
                "-pr",
                new WarriorsPredictor().PredictorName
            };

            predictionCLI.LoadArguments(args.ToArray());

            predictionCLI.RunPrediction();
        }

        [TestMethod]
        public void RunPrediction_InitProperlyEvaluate()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "test.txt",
                "-pr",
                new WarriorsPredictor().PredictorName,
                "-e"
            };

            predictionCLI.LoadArguments(args.ToArray());

            predictionCLI.RunPrediction();
        }

        [TestMethod]
        public void RunPrediction_InitWithActualModel_Predict()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "prediction.txt",
                "-pr",
                new WarriorsPredictor().PredictorName
            };

            predictionCLI.LoadArguments(args.ToArray());

            predictionCLI.RunPrediction();
        }

        [TestMethod]
        public void RunPrediction_InitWithActualModel_Evaluate()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "prediction.txt",
                "-pr",
                new WarriorsPredictor().PredictorName,
                "-e"
            };

            predictionCLI.LoadArguments(args.ToArray());

            predictionCLI.RunPrediction();
        }

        [TestMethod]
        public void RunPrediction_InitWithInvalidPredictor()
        {
            var predictionCLI = new PredictionCLI();

            Assert.IsNotNull(predictionCLI);

            var args = new List<string>
            {
                "-pd",
                "prediction.txt",
                "-pr",
                "wick"
            };

            predictionCLI.LoadArguments(args.ToArray());

            predictionCLI.RunPrediction();
        }
    }
}