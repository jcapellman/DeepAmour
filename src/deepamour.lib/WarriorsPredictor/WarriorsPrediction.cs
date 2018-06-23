using deepamour.lib.Base;
using deepamour.lib.WarriorsPredictor.Objects;

namespace deepamour.lib.WarriorsPredictor
{
    public class WarriorsPrediction : BaseFastTreePredictor<WarriorsData, WarriorsDataPrediction>
    {
        protected override string ModelName => "warriors.mdl";

        protected override string PredictorColumn => "WarriorsWin";

        public override string DisplayPrediction(WarriorsDataPrediction prediction) => $"Warriors Win: {(prediction.WarriorsWin > 0 ? "Yes" : "No")}";

        public WarriorsPrediction(string modelData) : base(modelData) { }
    }
}