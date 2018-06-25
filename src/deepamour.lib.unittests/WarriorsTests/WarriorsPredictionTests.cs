using System;

using deepamour.lib.core.Predictors.WarriorsPredictor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.unittests.WarriorsTests
{
    [TestClass]
    public class WarriorsPredictionTests
    {
        [TestMethod]
        public void AbstractCheck_PredictorName()
        {
            var predictor = new WarriorsPredictor();

            Assert.IsNotNull(predictor.PredictorName);
        }

        [TestMethod]
        public void AbstractCheck_PredictorPrettyName()
        {
            var predictor = new WarriorsPredictor();

            Assert.IsNotNull(predictor.PredictorPrettyName);
        }

        [TestMethod]
        public void AbstractCheck_PredictorColumn()
        {
            var predictor = new WarriorsPredictor();

            Assert.IsNotNull(predictor.PredictorColumn);
        }

        [TestMethod]
        public void InitAndPredict_Null()
        {
            var predictor = new WarriorsPredictor();

            var prediction = predictor.RunPredictorAsync(null, null).Result;

            Assert.IsNotNull(prediction);
            Assert.IsTrue(prediction.IsNullOrError);
        }

        [TestMethod]
        public void InitAndPredict_NotExistingPredicatorDataFile()
        {
            var predictor = new WarriorsPredictor();

            var prediction = predictor.RunPredictorAsync($"{DateTime.Now.Ticks}.txt", null).Result;

            Assert.IsNotNull(prediction);
            Assert.IsTrue(prediction.IsNullOrError);
        }

        [TestMethod]
        public void InitAndPredict_NotExistingPredicatorDataFileOrTrainingDataFile()
        {
            var predictor = new WarriorsPredictor();

            var prediction = predictor.RunPredictorAsync($"{DateTime.Now.Ticks}.txt", $"{DateTime.Now.Ticks}.txt").Result;

            Assert.IsNotNull(prediction);
            Assert.IsTrue(prediction.IsNullOrError);
        }
    }
}