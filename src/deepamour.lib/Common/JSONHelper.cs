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
            catch (JsonReaderException ex)
            {
                return new ReturnObj<T>(ex);
            }
        }

        public static ReturnObj<string> SerializeFromJson<T>(this BaseDataPrediction predictionObject) => 
            predictionObject == null ? 
                new ReturnObj<string>(new ArgumentNullException(nameof(predictionObject))) : 
                new ReturnObj<string>(JsonConvert.SerializeObject(predictionObject));
    }
}