using System.IO;

using Microsoft.ML;

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
        
        protected abstract string PredictorColumn { get; }
        
        protected PredictionModel<T, TK> Model;

        protected abstract void LoadDataAsync();

        public TK Predict(string predictorDataFileName)
        {
            if (Model == null)
            {
                LoadDataAsync();
            }

            var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(predictorDataFileName));

            return Model.Predict(data);
        }
    }
}