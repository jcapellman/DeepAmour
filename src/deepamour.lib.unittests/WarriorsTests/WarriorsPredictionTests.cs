using System;

using deepamour.lib.Predictors.WarriorsPredictor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.unittests.WarriorsTests
{
    [TestClass]
    public class WarriorsPredictionTests
    {
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