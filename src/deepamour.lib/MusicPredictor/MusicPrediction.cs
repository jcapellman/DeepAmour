using System.IO;
using System.Threading.Tasks;

using deepamour.lib.MusicPredictor.Objects;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace deepamour.lib.MusicPredictor
{
    public class MusicPrediction
    {
        private const string MODEL_NAME = "music.mdl";

        private PredictionModel<MusicData, MusicDataPrediction> _model;

        public async Task<bool> LoadDataAsync(string fileName)
        {
            if (File.Exists(MODEL_NAME))
            {
                _model = await PredictionModel.ReadAsync<MusicData, MusicDataPrediction>(MODEL_NAME);
                
                return true;
            }

            var pipeline = new LearningPipeline
            {
                new TextLoader(fileName).CreateFrom<MusicData>(separator: ','),
                new TextFeaturizer("Features", "SentimentText"),
                new FastTreeBinaryClassifier {NumLeaves = 5, NumTrees = 5, MinDocumentsInLeafs = 2}
            };

            _model = pipeline.Train<MusicData, MusicDataPrediction>();

            await _model.WriteAsync("music.mdl");

            return true;
        }

        public MusicDataPrediction Run(MusicData data) => _model.Predict(data);
    }
}