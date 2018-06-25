using System;
using System.Collections.Generic;
using System.Linq;

using deepamour.lib.Common;

namespace deepamour.lib.cli.Common
{
    public static class CommandLineParser
    {
        public class CommandLineArguments
        {
            public string TrainingDataFileName { get; set; }

            public string Predictor { get; set; }

            public string PredictionDataFileName { get; set; }

            public bool Evaluate { get; set; }
        }

        public static ReturnObj<CommandLineArguments> ParseArguments(IReadOnlyList<string> args)
        {
            try
            {
                if (args == null)
                {
                    throw new ArgumentNullException(nameof(args));
                }

                var commandLine = new CommandLineArguments();

                if (!args.Any())
                {
                    return new ReturnObj<CommandLineArguments>(commandLine);
                }

                for (var x = 0; x < args.Count; x += 2)
                {
                    switch (args[x].ToLower())
                    {
                        case "-pd":
                            commandLine.PredictionDataFileName = args[x + 1];
                            break;
                        case "-pr":
                            commandLine.Predictor = args[x + 1];
                            break;
                        case "-td":
                            commandLine.TrainingDataFileName = args[x + 1];
                            break;
                        case "-e":
                            commandLine.Evaluate = true;
                            break;
                    }
                }

                return new ReturnObj<CommandLineArguments>(commandLine);
            }
            catch (Exception ex)
            {
                return new ReturnObj<CommandLineArguments>(ex);
            }
        }
    }
}