using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.WarriorsPredictor.Objects
{
    public class WarriorsDataPrediction
    {
        [ColumnName("Label")]
        public int WarriorsWin;

        [ColumnName("Score")]
        public float[] Score;
    }
}