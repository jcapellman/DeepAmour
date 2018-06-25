using deepamour.lib.core.Managers;
using deepamour.lib.core.Predictors.WarriorsPredictor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.unittests.ManagerTests
{
    [TestClass]
    public class PredictorManagerTests
    {
        [TestMethod]
        public void PredictorManager_NullRetrieval()
        {
            var result = PredictorManager.GetPredictor(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PredictorManager_ValidRetrieval()
        {
            var result = PredictorManager.GetPredictor(new WarriorsPredictor().PredictorName);

            Assert.IsNotNull(result);
        }
    }
}