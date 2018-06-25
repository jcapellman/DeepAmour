using System;

namespace deepamour.lib.core.Common
{
    public class ReturnObj<T>
    {
        private readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public T Value { get; private set; }

        public bool IsNullOrError => Value == null || Error != null;

        public Exception Error { get; private set; }

        public ReturnObj(T obj)
        {
            Value = obj;
        }

        public ReturnObj(Exception error)
        {
            Value = default(T);
            Error = error;

            Log.Error(error);
        }
    }
}
