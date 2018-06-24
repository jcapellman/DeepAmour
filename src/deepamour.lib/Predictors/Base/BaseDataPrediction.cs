using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.Predictors.Base
{
    public class BaseDataPrediction
    {
        [ColumnName("Score")]
        public float Score;
    }
}