using System;

using deepamour.lib.MusicPredictor;
using deepamour.lib.MusicPredictor.Objects;

namespace deepamour.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var predictor = new MusicPrediction(args[0]);
            
            var result = predictor.Predict(new MusicData
            {
                SentimentText = "Enter Sandman"
            });
            
            Console.WriteLine($"Prediction: {result.Sentiment}");
        }
    }
}