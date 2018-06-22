using deepamour.lib.WarriorsPredictor.Objects;

namespace deepamour.lib.NBAPredictor
{
    public class WarriorsPrediction : BasePrediction<WarriorsData, WarriorsDataPrediction>
    {
        protected override string ModelName => "warriors.mdl";

        protected override string PredictorColumn => "WarriorsWin";

        public override string DisplayPrediction(WarriorsDataPrediction prediction) => $"Warriors Win: {prediction}";

        public WarriorsPrediction(string modelData) : base(modelData) { }
    }
}