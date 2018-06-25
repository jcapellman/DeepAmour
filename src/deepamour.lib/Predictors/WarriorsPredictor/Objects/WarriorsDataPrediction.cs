using deepamour.lib.core.Predictors.Base;

using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.core.Predictors.WarriorsPredictor.Objects
{
    public class WarriorsDataPrediction : BaseDataPrediction
    {
        [ColumnName("Label")]
        public float WarriorsWin;
    }
}