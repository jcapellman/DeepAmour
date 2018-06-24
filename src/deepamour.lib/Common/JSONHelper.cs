using System;

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
    }
}