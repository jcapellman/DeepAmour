using deepamour.lib.MusicPredictor.Objects;

namespace deepamour.lib.MusicPredictor
{
    public class MusicPrediction : BasePrediction<MusicData, MusicDataPrediction>
    {
        public override string DisplayPrediction(MusicDataPrediction prediction) => $"Will you like it: {prediction.Sentiment}";

        protected override string ModelName => "music.mdl";
        protected override string PredictorColumn => "Like";

        public MusicPrediction(string trainingFile) : base(trainingFile)
        {
        }
    }
}