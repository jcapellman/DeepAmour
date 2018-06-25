using System.Threading.Tasks;

using deepamour.lib.Common;
using deepamour.lib.Predictors.Base;

using Microsoft.ML;
using Microsoft.ML.Models;

namespace deepamour.lib.Base
{
    public abstract class BasePrediction
    {
        protected abstract string ModelName { get; }

        public abstract string PredictorName { get; }

        public abstract string PredictorPrettyName { get; }

        protected internal abstract string PredictorColumn { get; }

        protected abstract Task<ReturnObj<PredictionModel<T, TK>>> LoadOrGenerateModelAsync<T, TK>(string trainingFileName) where T : BasePredictorData where TK : BaseDataPrediction, new();

        protected abstract ReturnObj<RegressionMetrics> EvaluateModel<T, TK>(PredictionModel<T, TK> model, string testDataFilePath) where T : BasePredictorData where TK : BaseDataPrediction, new();

        protected abstract ReturnObj<TK> Predict<T, TK>(PredictionModel<T, TK> model, string predictorDataFileName) where T : BasePredictorData where TK : BaseDataPrediction, new();
    }
}