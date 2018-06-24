using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.unittests.WarriorsTests
{
    [TestClass]
    public class WarriorsPredictionTests
    {
        [TestMethod]
        public void InitAndPredict_Null()
        {
            var predictor = new WarriorsPredictor.WarriorsPrediction(null);

            Assert.IsNotNull(predictor);

            var model = predictor.Predict(null);

            Assert.IsNotNull(model);
            Assert.IsTrue(model.IsNullOrError);
        }

        [TestMethod]
        public void Init_Null()
        {
            var predictor = new WarriorsPredictor.WarriorsPrediction(null);

            Assert.IsNotNull(predictor);
        }

        [TestMethod]
        public void Init_EmptyString()
        {
            var predictor = new WarriorsPredictor.WarriorsPrediction(string.Empty);

            Assert.IsNotNull(predictor);
        }

        [TestMethod]
        public void InitAndPredict_NotExistingFile()
        {
            var predictor = new WarriorsPredictor.WarriorsPrediction($"{DateTime.Now.Ticks}.txt");

            Assert.IsNotNull(predictor);

            var prediction = predictor.Predict($"{DateTime.Now.Ticks}.txt");

            Assert.IsTrue(prediction.IsNullOrError);
            Assert.IsTrue(prediction.Error.GetType() == typeof(FileNotFoundException));
        }
    }
}