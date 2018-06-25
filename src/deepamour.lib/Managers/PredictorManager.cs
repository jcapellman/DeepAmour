using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using deepamour.lib.core.Base;

namespace deepamour.lib.core.Managers
{
    public static class PredictorManager
    {
        private static List<BaseFastTreePredictor> _predictors;

        public static List<BaseFastTreePredictor> Predictors
        {
            get
            {
                if (_predictors != null)
                {
                    return _predictors;
                }

                _predictors = Assembly.GetAssembly(typeof(PredictorManager)).GetTypes()
                    .Where(a => !a.IsAbstract && a.BaseType == typeof(BaseFastTreePredictor))
                    .Select(a => (BaseFastTreePredictor) Activator.CreateInstance(a)).ToList();

                return _predictors;
            }
        }

        public static BaseFastTreePredictor GetPredictor(string predictorName) => Predictors.FirstOrDefault(a =>
            string.Equals(a.PredictorName, predictorName, StringComparison.InvariantCultureIgnoreCase));
    }
}