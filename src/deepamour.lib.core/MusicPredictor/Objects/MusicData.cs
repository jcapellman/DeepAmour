using Microsoft.ML.Runtime.Api;

namespace deepamour.lib.MusicPredictor.Objects
{
    public class MusicData
    {
        [Column("0")]
        public string SentimentText;

        [Column("1", name: "Label")]
        public float Sentiment;
    }
}