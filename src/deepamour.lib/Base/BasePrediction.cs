using Microsoft.ML;

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
        
        protected PredictionModel<T, TK> _model;

        protected abstract void LoadDataAsync();

        public TK Predict(T data)
        {
            if (_model == null)
            {
                LoadDataAsync();
            }

            return _model.Predict(data);
        }
    }
}