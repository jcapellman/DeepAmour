using System.Threading.Tasks;

using deepamour.lib.core.Base;
using deepamour.lib.core.Common;
using deepamour.lib.core.Predictors.WarriorsPredictor.Objects;

using Microsoft.ML;
using Microsoft.ML.Models;

namespace deepamour.lib.core.Predictors.WarriorsPredictor
{
    public class WarriorsPredictor : BaseFastTreePredictor
    {
        private PredictionModel<WarriorsData, WarriorsDataPrediction> _model;

        protected override string ModelName => "warriors.mdl";

        public override string PredictorName => "warriors";

        public override string PredictorPrettyName => "Warriors Predictor";

        protected internal override string PredictorColumn => "WarriorsWin";

        public override async Task<ReturnObj<RegressionMetrics>> RunEvaluationAsync(string testDataFilePath)
        {
            var modelLoadResult = await LoadModelAsync(testDataFilePath);

            return modelLoadResult.IsNullOrError ? new ReturnObj<RegressionMetrics>(modelLoadResult.Error) : EvaluateModel(modelLoadResult.Value, testDataFilePath);
        }

        private async Task<ReturnObj<PredictionModel<WarriorsData, WarriorsDataPrediction>>> LoadModelAsync(string testDataFilePath)
        {
            if (_model != null)
            {
                return new ReturnObj<PredictionModel<WarriorsData, WarriorsDataPrediction>>(_model);
            }

            var model = await LoadOrGenerateModelAsync<WarriorsData, WarriorsDataPrediction>(testDataFilePath);

            if (model.IsNullOrError)
            {
                return new ReturnObj<PredictionModel<WarriorsData, WarriorsDataPrediction>>(model.Error);
            }

            _model = model.Value;

            return new ReturnObj<PredictionModel<WarriorsData, WarriorsDataPrediction>>(_model);
        }

        public override async Task<ReturnObj<string>> RunPredictorAsync(string predictorDataFileName, string testDataFilePath = null)
        {
            var modelLoadResult = await LoadModelAsync(testDataFilePath);

            if (modelLoadResult.IsNullOrError)
            {
                return new ReturnObj<string>(modelLoadResult.Error);
            }

            var prediction = Predict(modelLoadResult.Value, predictorDataFileName);

            if (prediction.IsNullOrError)
            {
                return new ReturnObj<string>(prediction.Error);
            }

            var serialized = prediction.Value.SerializeFromJson<WarriorsDataPrediction>();

            return serialized.IsNullOrError ? new ReturnObj<string>(serialized.Error) : new ReturnObj<string>(serialized.Value);
        }
    }
}