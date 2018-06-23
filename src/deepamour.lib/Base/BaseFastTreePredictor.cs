using System.IO;
using System.Linq;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace deepamour.lib.Base
{
    public class BaseFastTreePredictor<T, TK> : BasePrediction<T, TK> where T : class where TK : class, new()
    {
        protected BaseFastTreePredictor(string trainingData) : base(trainingData) { }

        protected override string ModelName => throw new System.NotImplementedException();

        protected override string PredictorColumn => throw new System.NotImplementedException();

        public override string DisplayPrediction(TK prediction)
        {
            throw new System.NotImplementedException();
        }

        protected override async void LoadDataAsync()
        {
            if (File.Exists(ModelName))
            {
                Model = await PredictionModel.ReadAsync<T, TK>(ModelName);

                return;
            }

            var inputColumns = typeof(T).GetFields().Where(a => a.FieldType != typeof(float)).Select(a => a.Name).ToArray();

            var pipeline = new LearningPipeline
            {
                new TextLoader(TrainingFile).CreateFrom<T>(separator: ','),
                new ColumnConcatenator("Features", "Features"),
                new FastTreeRegressor()
            };

            Model = pipeline.Train<T, TK>();

            await Model.WriteAsync(ModelName);
        }
    }
}