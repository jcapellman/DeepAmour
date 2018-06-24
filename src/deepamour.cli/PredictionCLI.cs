using System;
using System.Collections.Generic;

using deepamour.lib.WarriorsPredictor;

using NLog;

namespace deepamour.cli
{
    public class PredictionCLI
    {
        private static Logger Log => NLog.LogManager.GetCurrentClassLogger();

        private CommandLineArguments _arguments;

        private class CommandLineArguments
        {
            public string TrainingDataFileName { get; set; }

            public string Predictor { get; set; }

            public string PredictionDataFileName { get; set; }

            public bool Evaluate { get; set; }
        }

        private CommandLineArguments ParseArguments(IReadOnlyList<string> args)
        {
            var commandLine = new CommandLineArguments();

            for (var x = 0; x < args.Count; x += 2)
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

        private void DisplayArgumentsHelp()
        {
            Console.WriteLine("DeepAmour Command Line Arguments:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("-p <filename> (Prediction Data File Name, required)");
            Console.WriteLine("-pr <predictor name> (Predictor Name, required)");
            Console.WriteLine("-d <filename> (Training Data, required if model doesn't exist)");
            Console.WriteLine("-e (Evaluate the Model, Prediction Data is required)");

            Console.WriteLine(System.Environment.NewLine);
        }

        private WarriorsPrediction LoadPredictor(string predictor, string trainingDataFileName)
        {
            switch (predictor.ToLower())
            {
                case "warriors":
                    return new WarriorsPrediction(trainingDataFileName);
            }

            return null;
        }

        private bool ValidateCommandLine(CommandLineArguments arguments)
        {
            if (string.IsNullOrEmpty(arguments.PredictionDataFileName) || string.IsNullOrEmpty(arguments.Predictor))
            {
                DisplayArgumentsHelp();

                Log.Error($"Prediction Data and/or Predictor command line arguments are null");

                return false;
            }

            if (arguments.Evaluate && string.IsNullOrEmpty(arguments.PredictionDataFileName))
            {
                DisplayArgumentsHelp();

                return false;
            }

            return true;
        }

        public bool LoadArguments(string[] args)
        {
            var arguments = ParseArguments(args);

            if (arguments == null)
            {
                return false;
            }

            if (!ValidateCommandLine(arguments))
            {
                return false;
            }

            _arguments = arguments;

            return true;
        }

        public async void RunPrediction()
        {
            var predictor = LoadPredictor(_arguments.Predictor, _arguments.TrainingDataFileName);

            if (predictor == null)
            {
                Console.WriteLine($"{_arguments.Predictor} predictor was not found in the Library");

                return;
            }

            if (_arguments.Evaluate)
            {
                var metrics = predictor.EvaluateModel(_arguments.PredictionDataFileName);

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
                var result = await predictor.PredictAsync(_arguments.PredictionDataFileName);

                Console.WriteLine(predictor.DisplayPrediction(result.Value));
            }
        }
    }
}