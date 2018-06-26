using System;

using deepamour.lib.cli.Common;
using deepamour.lib.core.Managers;

using NLog;

namespace deepamour.lib.cli
{
    public class PredictionCLI
    {
        private static Logger Log => LogManager.GetCurrentClassLogger();

        private CommandLineParser.CommandLineArguments _arguments;

        private void DisplayArgumentsHelp()
        {
            Console.WriteLine("DeepAmour Command Line Arguments:");
            Console.WriteLine("-----------------------");
            Console.WriteLine("-p <filename> (Prediction Data File Name, required)");
            Console.WriteLine("-pr <predictor name> (Predictor Name, required)");
            Console.WriteLine("-d <filename> (Training Data, required if model doesn't exist)");
            Console.WriteLine("-e (Evaluate the Model, Prediction Data is required)");

            Console.WriteLine(Environment.NewLine);
        }

        internal bool ValidateCommandLine(CommandLineParser.CommandLineArguments arguments)
        {
            if (arguments == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(arguments.PredictionDataFileName) || string.IsNullOrEmpty(arguments.Predictor))
            {
                DisplayArgumentsHelp();

                Log.Error("Prediction Data and/or Predictor command line arguments are null");

                return false;
            }

            if (!arguments.Evaluate || !string.IsNullOrEmpty(arguments.PredictionDataFileName))
            {
                return true;
            }

            DisplayArgumentsHelp();

            return false;
        }

        public bool LoadArguments(string[] args)
        {
            var arguments = CommandLineParser.ParseArguments(args);

            if (arguments.IsNullOrError)
            {
                return false;
            }

            if (!ValidateCommandLine(arguments.Value))
            {
                return false;
            }

            _arguments = arguments.Value;

            return true;
        }

        public async void RunPrediction()
        {
            var predictor = PredictorManager.GetPredictor(_arguments.Predictor);

            if (predictor == null)
            {
                Console.WriteLine($"{_arguments.Predictor} predictor was not found in the Library");
                
                Console.WriteLine(string.Empty);

                Console.WriteLine("Available Predictors:");

                foreach (var availablePredictor in PredictorManager.Predictors)
                {
                    Console.WriteLine($"{availablePredictor.PredictorName} ({availablePredictor.PredictorPrettyName})");
                }

                return;
            }

            if (_arguments.Evaluate)
            {
                var metrics = await predictor.RunEvaluationAsync(_arguments.PredictionDataFileName);

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
                var result = await predictor.RunPredictorAsync(_arguments.PredictionDataFileName, _arguments.TrainingDataFileName);

                if (result.IsNullOrError)
                {
                    Console.WriteLine("Failed to run model");

                    return;
                }

                Console.WriteLine(result.Value);
            }
        }
    }
}