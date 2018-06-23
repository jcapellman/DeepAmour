using System;
using System.Collections.Generic;

using deepamour.lib.WarriorsPredictor;

namespace deepamour.cli
{
    class Program
    {
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
            Console.WriteLine("-pr <predictor name> (Predictor Name, optional)");
            Console.WriteLine("-d <filename> (Training Data, required if model doesn't exist)");
            Console.WriteLine("-e (Evaluate the Model, Prediction Data is required");

            Console.WriteLine(System.Environment.NewLine);
        }

        static void Main(string[] args)
        {
            var commandLine = ParseArguments(args);

            if (string.IsNullOrEmpty(commandLine.PredictionDataFileName))
            {
                DisplayArgumentsHelp();

                return;
            }

            if (commandLine.Evaluate && string.IsNullOrEmpty(commandLine.TrainingDataFileName))
            {
                DisplayArgumentsHelp();

                return;
            }

            var predictor = new WarriorsPrediction(commandLine.TrainingDataFileName);

            if (commandLine.Evaluate)
            {
                var metrics = predictor.EvaluateModel(commandLine.PredictionDataFileName);

                Console.WriteLine($"L1: {metrics.L1}");
                Console.WriteLine($"L2: {metrics.L2}");
                Console.WriteLine($"LossFn: {metrics.LossFn}");

                Console.WriteLine(System.Environment.NewLine);
            }
            else
            {
                var result = predictor.Predict(commandLine.PredictionDataFileName);

                Console.WriteLine(predictor.DisplayPrediction(result));
            }
        }
    }
}