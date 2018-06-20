using System;
using System.Threading.Tasks;

using deepamour.lib.MusicPredictor;
using deepamour.lib.MusicPredictor.Objects;

namespace deepamour.cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var predictor = new MusicPrediction();

            var dataFilename = args[0];

            var dataLoad = await predictor.LoadDataAsync(dataFilename);

            var result = predictor.Run(new MusicData
            {
                SentimentText = "Enter Sandman"
            });
            
            Console.WriteLine($"Prediction: {result.Sentiment}");
        }
    }
}