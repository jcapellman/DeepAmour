using System.Threading.Tasks;

using deepamour.lib.Base;
using deepamour.lib.Common;
using deepamour.lib.Predictors.WarriorsPredictor.Objects;

using Microsoft.ML.Models;

namespace deepamour.lib.Predictors.WarriorsPredictor
{
    public class WarriorsPredictor : BaseFastTreePredictor
    {
        protected override string ModelName => "warriors.mdl";

        public override string PredictorName => "warriors";

        public override string PredictorPrettyName => "Warriors Predictor";

        protected override string PredictorColumn => "WarriorsWin";

        public override async Task<ReturnObj<RegressionMetrics>> RunEvaluationAsync(string testDataFilePath)
        {
            var model = await LoadOrGenerateModelAsync<WarriorsData, WarriorsDataPrediction>(testDataFilePath);

            return model.IsNullOrError ? new ReturnObj<RegressionMetrics>(model.Error) : EvaluateModel(model.Value, testDataFilePath);
        }

        public override async Task<ReturnObj<string>> RunPredictorAsync(string predictorDataFileName, string testDataFilePath = null)
        {
            var model = await LoadOrGenerateModelAsync<WarriorsData, WarriorsDataPrediction>(testDataFilePath);

            if (model.IsNullOrError)
            {
                return new ReturnObj<string>(model.Error);
            }

            var prediction = Predict(model.Value, predictorDataFileName);

            if (prediction.IsNullOrError)
            {
                return new ReturnObj<string>(prediction.Error);
            }

            var serialized = prediction.Value.SerializeFromJson<WarriorsDataPrediction>();

            return serialized.IsNullOrError ? new ReturnObj<string>(serialized.Error) : new ReturnObj<string>(serialized.Value);
        }
    }
}