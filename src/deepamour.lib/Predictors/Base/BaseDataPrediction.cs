using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.core.Predictors.Base
{
    public class BaseDataPrediction
    {
        [ColumnName("Score")]
        public float Score;
    }
}