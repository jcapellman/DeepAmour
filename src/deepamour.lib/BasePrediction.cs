using System.IO;

using deepamour.lib.MusicPredictor.Objects;

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

        protected abstract string ModelName { get; }

        protected abstract string OutputColumn { get; }

        protected abstract string[] InputColumns { get; }

        private PredictionModel<T, TK> _model;

        private async void LoadDataAsync()
        {
            if (File.Exists(ModelName))
            {
                _model = await PredictionModel.ReadAsync<T, TK>(ModelName);
            }

            var pipeline = new LearningPipeline
            {
                new TextLoader(_trainingFile).CreateFrom<MusicData>(separator: ','),
                new TextFeaturizer(OutputColumn, InputColumns),
                new FastTreeBinaryClassifier
                {
                    NumLeaves = 5,
                    NumTrees = 5,
                    MinDocumentsInLeafs = 2
                }
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