using System;
using System.IO;

using deepamour.lib.Common;

using Microsoft.ML;
using Microsoft.ML.Models;

using Newtonsoft.Json;

namespace deepamour.lib.Base
{
    public abstract class BasePrediction<T, TK> where T : class where TK : class, new()
    {
        protected readonly string TrainingFile;

        protected BasePrediction(string trainingFile)
        {
            TrainingFile = trainingFile;
        }

        public abstract string DisplayPrediction(TK prediction);

        protected abstract string ModelName { get; }

        public abstract string PredictorName { get; }

        protected abstract string PredictorColumn { get; }
        
        protected PredictionModel<T, TK> Model;

        protected abstract void LoadDataAsync();

        public ReturnObj<TK> Predict(string predictorDataFileName)
        {
            if (Model == null)
            {
                LoadDataAsync();
            }

            if (string.IsNullOrEmpty(predictorDataFileName))
            {
                return new ReturnObj<TK>(new Exception("predictorDataFileName is not set"));
            }

            if (!File.Exists(predictorDataFileName))
            {
                return new ReturnObj<TK>(new FileNotFoundException($"{predictorDataFileName} was not found to be used as prediction data"));
            }
            
            var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(predictorDataFileName));

            return new ReturnObj<TK>(Model.Predict(data));
        }

        public abstract ReturnObj<RegressionMetrics> EvaluateModel(string testDataFilePat);
    }
}