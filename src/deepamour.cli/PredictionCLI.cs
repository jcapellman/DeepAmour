using System;
using System.Linq;
using System.Reflection;

using deepamour.cli.Common;
using deepamour.lib.Base;
using deepamour.lib.Common;

using NLog;

namespace deepamour.cli
{
    public class PredictionCLI
    {
        private static Logger Log => NLog.LogManager.GetCurrentClassLogger();

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

        private static BaseFastTreePredictor LoadPredictor(string predictorName)
        {
            var predictors = Assembly.GetAssembly(typeof(JSONHelper)).GetTypes()
                .Where(a => !a.IsAbstract && a.BaseType == typeof(BaseFastTreePredictor)).Select(a => (BaseFastTreePredictor)Activator.CreateInstance(a)).ToList();

            return predictors.FirstOrDefault(a => string.Equals(a.PredictorName, predictorName, StringComparison.CurrentCultureIgnoreCase));
        }

        private bool ValidateCommandLine(CommandLineParser.CommandLineArguments arguments)
        {
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
            var predictor = LoadPredictor(_arguments.Predictor);

            if (predictor == null)
            {
                Console.WriteLine($"{_arguments.Predictor} predictor was not found in the Library");

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