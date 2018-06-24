﻿using System.IO;

using deepamour.lib.Common;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Models;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;

namespace deepamour.lib.Base
{
    public class BaseFastTreePredictor<T, TK> : BasePrediction<T, TK> where T : class where TK : class, new()
    {
        protected BaseFastTreePredictor(string trainingData) : base(trainingData) { }

        protected override string ModelName => throw new System.NotImplementedException();

        public override string PredictorName => throw new System.NotImplementedException();

        protected override string PredictorColumn => throw new System.NotImplementedException();

        public override string DisplayPrediction(TK prediction) => throw new System.NotImplementedException();

        protected override async void LoadDataAsync()
        {
            if (File.Exists(ModelName))
            {
                Model = await PredictionModel.ReadAsync<T, TK>(ModelName);

                return;
            }

            var pipeline = new LearningPipeline
            {
                new TextLoader(TrainingFile).CreateFrom<T>(separator: ','),
                new ColumnConcatenator("Features", "Features"),
                new FastTreeRegressor()
            };

            Model = pipeline.Train<T, TK>();

            await Model.WriteAsync(ModelName);
        }

        public override ReturnObj<RegressionMetrics> EvaluateModel(string testDataFilePath)
        {
            var evaluator = new RegressionEvaluator();

            var testData = new TextLoader(testDataFilePath).CreateFrom<T>();

            return new ReturnObj<RegressionMetrics>(evaluator.Evaluate(Model, testData));
        }
    }
}