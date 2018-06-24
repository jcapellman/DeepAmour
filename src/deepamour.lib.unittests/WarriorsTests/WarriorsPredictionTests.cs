using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.unittests.WarriorsTests
{
    [TestClass]
    public class WarriorsPredictionTests
    {
        [TestMethod]
        public void Init_Null()
        {
            var prediction = new WarriorsPredictor.WarriorsPrediction(null);

            Assert.IsNotNull(prediction);

            var model = prediction.Predict(null);

            Assert.IsNotNull(model);
        }
    }
}