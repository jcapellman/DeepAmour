using deepamour.lib.Predictors.Base;

using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.Predictors.WarriorsPredictor.Objects
{
    public class WarriorsDataPrediction : BaseDataPrediction
    {
        [ColumnName("Label")]
        public float WarriorsWin;
    }
}