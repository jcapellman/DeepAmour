using deepamour.lib.MusicPredictor.Objects;

namespace deepamour.lib.MusicPredictor
{
    public class MusicPrediction : BasePrediction<MusicData, MusicDataPrediction>
    {
        protected override string ModelName => "music.mdl";

        protected override string OutputColumn => "Features";

        protected override string[] InputColumns => new[] { "SentimentText" };

        public MusicPrediction(string trainingFile) : base(trainingFile)
        {
        }
    }
}