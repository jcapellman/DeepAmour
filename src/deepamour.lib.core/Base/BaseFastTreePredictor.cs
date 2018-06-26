using System;
using System.IO;
using System.Threading.Tasks;

using deepamour.lib.core.Common;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace deepamour.lib.core.Base
{
    public abstract class BaseFastTreePredictor : BasePrediction
    {        

        public abstract Task<ReturnObj<RegressionMetrics>> RunEvaluationAsync(string testDataFilePath);

        public abstract Task<ReturnObj<string>> RunPredictorAsync(string predictorDataFileName, string testDataFilePath = null);

        internal override async Task<ReturnObj<PredictionModel<T, TK>>> LoadOrGenerateModelAsync<T, TK>(string trainingFileName)
        {
            PredictionModel<T, TK> model;

            if (File.Exists(ModelName))
            {
                model = await PredictionModel.ReadAsync<T, TK>(ModelName);

                return new ReturnObj<PredictionModel<T, TK>>(model);
            }

            try
            {
                var pipeline = new LearningPipeline
                {
                    new TextLoader(trainingFileName).CreateFrom<T>(separator: ','),
                    new ColumnConcatenator("Features", "Features"),
                    new FastTreeRegressor()
                };

                model = pipeline.Train<T, TK>();

                await model.WriteAsync(ModelName);
            }
            catch (Exception ex)
            {
                return new ReturnObj<PredictionModel<T, TK>>(ex);
            }

            return new ReturnObj<PredictionModel<T, TK>>(model);
        }

        internal override ReturnObj<RegressionMetrics> EvaluateModel<T, TK>(PredictionModel<T, TK> model, string testDataFilePath)
        {
            var evaluator = new RegressionEvaluator();

            var testData = new TextLoader(testDataFilePath).CreateFrom<T>();

            return new ReturnObj<RegressionMetrics>(evaluator.Evaluate(model, testData));
        }

        internal override ReturnObj<TK> Predict<T, TK>(PredictionModel<T, TK> model, string predictorDataFileName)
        {
            if (model == null)
            {
                return new ReturnObj<TK>(new ArgumentNullException(nameof(model)));
            }

            if (string.IsNullOrEmpty(predictorDataFileName))
            {
                return new ReturnObj<TK>(new ArgumentNullException(nameof(predictorDataFileName)));
            }

            if (!File.Exists(predictorDataFileName))
            {
                return new ReturnObj<TK>(new FileNotFoundException($"{predictorDataFileName} was not found to be used as prediction data"));
            }

            var data = File.ReadAllText(predictorDataFileName).DeserializeFromJson<T>();

            return data.IsNullOrError ? new ReturnObj<TK>(data.Error) : new ReturnObj<TK>(model?.Predict(data.Value));
        }
    }
}