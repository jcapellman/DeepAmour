using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.WarriorsPredictor.Objects
{
    public class WarriorsDataPrediction
    {
        [ColumnName("Score")]
        public bool Win;
    }
}