using System;

using deepamour.lib.Predictors.Base;

using Newtonsoft.Json;

namespace deepamour.lib.Common
{
    public static class JSONHelper
    {
        public static ReturnObj<T> DeserializeFromJson<T>(this string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return new ReturnObj<T>(new ArgumentNullException(nameof(jsonString)));
            }

            try
            {
                return new ReturnObj<T>(JsonConvert.DeserializeObject<T>(jsonString));
            }
            catch (Exception ex)
            {
                return new ReturnObj<T>(ex);
            }
        }

        public static ReturnObj<string> SerializeFromJson<T>(this BaseDataPrediction predictionObject)
        {
            if (predictionObject == null)
            {
                return new ReturnObj<string>(new ArgumentNullException(nameof(predictionObject)));
            }

            try
            {
                return new ReturnObj<string>(JsonConvert.SerializeObject(predictionObject));
            }
            catch (Exception ex)
            {
                return new ReturnObj<string>(ex);
            }
        }
    }
}