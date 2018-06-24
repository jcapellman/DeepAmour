using System;
using System.Collections.Generic;

using deepamour.lib.WarriorsPredictor;

using NLog;

namespace deepamour.cli
{
    class Program
    {
        private static Logger Log => NLog.LogManager.GetCurrentClassLogger();

        private class CommandLineArguments
        {
            public string TrainingDataFileName { get; set; }

            public string Predictor { get; set; }

            public string PredictionDataFileName { get; set; }

            public bool Evaluate { get; set; }
        }

        private static CommandLineArguments ParseArguments(IReadOnlyList<string> args)
        {
            var commandLine = new CommandLineArguments();

            for (var x = 0; x < args.Count; x+=2)
            {
                switch (args[x].ToLower())
                {
                    case "-p":
                        commandLine.PredictionDataFileName = args[x + 1];
                        break;
                    case "-pr":
                        commandLine.Predictor = args[x + 1];
                        break;
                    case "-d":
                        commandLine.TrainingDataFileName = args[x + 1];
                        break;
                    case "-e":
                        commandLine.Evaluate = true;
                        break;
                }
            }

            return commandLine;
        }

        private static void DisplayArgumentsHelp()
        {
            Console.WriteLine("DeepAmour Command Line Arguments:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("-p <filename> (Prediction Data File Name, required)");
            Console.WriteLine("-pr <predictor name> (Predictor Name, required)");
            Console.WriteLine("-d <filename> (Training Data, required if model doesn't exist)");
            Console.WriteLine("-e (Evaluate the Model, Prediction Data is required)");

            Console.WriteLine(System.Environment.NewLine);
        }

        private static WarriorsPrediction LoadPredictor(string predictor, string trainingDataFileName)
        {
            switch (predictor.ToLower())
            {
                case "warriors":
                    return new WarriorsPrediction(trainingDataFileName);
            }

            return null;
        }

        static void Main(string[] args)
        {
            var commandLine = ParseArguments(args);

            if (string.IsNullOrEmpty(commandLine.PredictionDataFileName) || string.IsNullOrEmpty(commandLine.Predictor))
            {
                DisplayArgumentsHelp();

                Log.Error($"Prediction Data and/or Predictor command line arguments are null");

                return;
            }

            if (commandLine.Evaluate && string.IsNullOrEmpty(commandLine.PredictionDataFileName))
            {
                DisplayArgumentsHelp();

                return;
            }

            var predictor = LoadPredictor(commandLine.Predictor, commandLine.TrainingDataFileName);

            if (predictor == null)
            {
                Console.WriteLine($"{commandLine.Predictor} predictor was not found in the Library");

                return;
            }
            
            if (commandLine.Evaluate)
            {
                var metrics = predictor.EvaluateModel(commandLine.PredictionDataFileName);

                if (metrics.IsNullOrError)
                {
                    Console.WriteLine("Could not run evaluation due to an error");

                    return;
                }

                Console.WriteLine($"L1: {metrics.Value.L1}");
                Console.WriteLine($"L2: {metrics.Value.L2}");
                Console.WriteLine($"LossFn: {metrics.Value.LossFn}");

                Console.WriteLine(Environment.NewLine);
            }
            else
            {
                var result = predictor.Predict(commandLine.PredictionDataFileName);

                Console.WriteLine(predictor.DisplayPrediction(result.Value));
            }
        }
    }
}