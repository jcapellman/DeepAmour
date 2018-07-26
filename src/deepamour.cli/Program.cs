using deepamour.lib.cli;

namespace deepamour.cli
{
    public class Program
    {
        internal static void Run(string[] args)
        {
            var predictionCli = new PredictionCLI();

            if (!predictionCli.LoadArguments(args))
            {
                return;
            }

            predictionCli.RunPrediction();
        }

        static void Main(string[] args)
        {
            Run(args);
        }
    }
}