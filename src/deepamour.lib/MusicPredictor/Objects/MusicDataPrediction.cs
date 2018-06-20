using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;

namespace deepamour.lib.MusicPredictor.Objects
{
    public class MusicDataPrediction
    {
        [ColumnName("PredictedLabel")]
        public DvBool Sentiment;
    }
}