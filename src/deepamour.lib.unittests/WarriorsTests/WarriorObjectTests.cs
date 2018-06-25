using System.Linq;
using deepamour.lib.core.Predictors.WarriorsPredictor.Objects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.unittests.WarriorsTests
{
    [TestClass]
    public class WarriorObjectTests
    {
        [TestMethod]
        public void WarriorData_InitConstructor()
        {
            var result = new WarriorsData();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WarriorData_Init()
        {
            var result = new WarriorsData
            {
                WarriorsWin = 0.0f,
                DurantPoints = 13,
                GreenPoints = 12,
                IguodalaPoints = 122,
                ThompsonPoints = 12,
                CurryPoints = 33,
                Features = new[] { 33.0f, 13.0f, 12.0f, 12.0f, 122.0f, 0.0f },
                Label = 3.0f
            };

            Assert.IsNotNull(result);

            Assert.AreEqual(result.WarriorsWin, 0.0f);

            Assert.AreEqual(result.DurantPoints, 13);

            Assert.AreEqual(result.GreenPoints, 12);

            Assert.AreEqual(result.ThompsonPoints, 12);
            
            Assert.AreEqual(result.IguodalaPoints, 122);

            Assert.AreEqual(result.CurryPoints, 33);
            
            Assert.AreEqual(result.Label, 3.0f);

            Assert.IsTrue(result.Features.Any());
        }
    }
}