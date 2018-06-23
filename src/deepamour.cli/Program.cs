using System;

using deepamour.lib.WarriorsPredictor;
using deepamour.lib.WarriorsPredictor.Objects;

namespace deepamour.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var predictor = new WarriorsPrediction(args[0]);
            
            var result = predictor.Predict(new WarriorsData
            {
                CurryPoints = 10,
                DurantPoints = 20,
                GreenPoints = 30,
                IguodalaPoints = 22,
                ThompsonPoints = 11,
                WarriorsWin = 0
            });
            
            Console.WriteLine(predictor.DisplayPrediction(result));
        }
    }
}