using System.IO;
using System.Linq;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace deepamour.lib
{
    public abstract class BasePrediction<T, TK> where T : class where TK : class, new()
    {
        private readonly string _trainingFile;

        protected BasePrediction(string trainingFile)
        {
            _trainingFile = trainingFile;
        }

        public abstract string DisplayPrediction(TK prediction);

        protected abstract string ModelName { get; }
        
        protected abstract string PredictorColumn { get; }

        private PredictionModel<T, TK> _model;

        private async void LoadDataAsync()
        {
            if (File.Exists(ModelName))
            {
                _model = await PredictionModel.ReadAsync<T, TK>(ModelName);
            }

            var inputColumns = typeof(T).GetProperties().Select(a => a.Name).ToArray();

            var pipeline = new LearningPipeline
            {
                new TextLoader(_trainingFile).CreateFrom<T>(separator: ','),
                new ColumnCopier((PredictorColumn, "Label")),
                new ColumnConcatenator("Features", inputColumns),
                new FastTreeRegressor()
            };

            _model = pipeline.Train<T, TK>();

            await _model.WriteAsync(ModelName);
        }

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