using deepamour.lib.cli;

namespace deepamour.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var predictionCli = new PredictionCLI();

            if (!predictionCli.LoadArguments(args))
            {
                return;
            }

            predictionCli.RunPrediction();
        }
    }
}