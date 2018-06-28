using System;
using System.Threading.Tasks;

using deepamour.lib.core.Base;
using deepamour.lib.core.Common;
using deepamour.lib.core.Predictors.Base;

using Microsoft.ML;
using Microsoft.ML.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace deepamour.lib.core.unittests.PredictorTests
{
    [TestClass]
    public class BaseFastTreePredictorTests
    {
        public class TestFasterTreePredictor : BaseFastTreePredictor
        {
            public PredictionModel<BasePredictorData, BaseDataPrediction> _model;

            protected override string ModelName => "warriors.mdl";

            public override string PredictorName => string.Empty;

            public override string PredictorPrettyName => string.Empty;

            protected internal override string PredictorColumn => string.Empty;

            public bool ForceModelUnload = false;
            
            public override async Task<ReturnObj<RegressionMetrics>> RunEvaluationAsync(string testDataFilePath)
            {
                if (string.IsNullOrEmpty(testDataFilePath))
                {
                    return new ReturnObj<RegressionMetrics>(new ArgumentNullException(nameof(testDataFilePath)));
                }

                var model = await LoadModelAsync(testDataFilePath);

                return EvaluateModel(model.Value, testDataFilePath);
            }

            public async Task<ReturnObj<PredictionModel<BasePredictorData, BaseDataPrediction>>> LoadModelAsync(string testDataFilePath)
            {
                if (ForceModelUnload)
                {
                    return new ReturnObj<PredictionModel<BasePredictorData, BaseDataPrediction>>(new Exception("Made Null"));
                }

                if (_model != null)
                {
                    return new ReturnObj<PredictionModel<BasePredictorData, BaseDataPrediction>>(_model);
                }

                var model = await LoadOrGenerateModelAsync<BasePredictorData, BaseDataPrediction>(testDataFilePath);

                if (model.IsNullOrError)
                {
                    return new ReturnObj<PredictionModel<BasePredictorData, BaseDataPrediction>>(model.Error);
                }

                _model = model.Value;

                return new ReturnObj<PredictionModel<BasePredictorData, BaseDataPrediction>>(_model);
            }

            public override async Task<ReturnObj<string>> RunPredictorAsync(string predictorDataFileName,
                string testDataFilePath = null)
            {
                PredictionModel<BasePredictorData, BaseDataPrediction> model = null;

                if (!ForceModelUnload)
                {
                    var result = await LoadOrGenerateModelAsync<BasePredictorData, BaseDataPrediction>(testDataFilePath);

                    model = result.Value;
                }

                var prediction = Predict(model, null);
                
                var serialized = prediction.Value.SerializeFromJson<BaseDataPrediction>();

                return serialized.IsNullOrError ? new ReturnObj<string>(serialized.Error) : new ReturnObj<string>(serialized.Value);
            } 
        }

        [TestMethod]
        public void Predict_NullModel()
        {
            var predictor = new TestFasterTreePredictor();

            Assert.IsNotNull(predictor);

            var result = predictor.Predict<BasePredictorData, BaseDataPrediction>(null, null);

            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(ArgumentNullException));
        }

        [TestMethod]
        public void Predict_NullTestData()
        {
            var predictor = new TestFasterTreePredictor();

            var model = predictor.LoadOrGenerateModelAsync<BasePredictorData, BaseDataPrediction>(null).Result;

            var result = predictor.Predict(model.Value, null);

            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(ArgumentNullException));
        }

        [TestMethod]
        public void Predict_BaseTests()
        {
            var predictor = new TestFasterTreePredictor();

            Assert.IsTrue(string.IsNullOrEmpty(predictor.PredictorColumn));
            Assert.IsTrue(string.IsNullOrEmpty(predictor.PredictorName));
            Assert.IsTrue(string.IsNullOrEmpty(predictor.PredictorPrettyName));
            Assert.IsNull(predictor._model);

            predictor.ForceModelUnload = true;

            var result = predictor.RunEvaluationAsync(null).Result;
            Assert.IsTrue(result.IsNullOrError);
            Assert.IsTrue(result.Error.GetType() == typeof(ArgumentNullException));

            predictor.ForceModelUnload = false;
            try
            {
                var evalResult = predictor.RunEvaluationAsync("prediction.txt").Result;

                Assert.IsNull(evalResult);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }
    }
}
