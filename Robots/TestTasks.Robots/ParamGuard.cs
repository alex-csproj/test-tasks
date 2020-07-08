using System;

namespace TestTasks.Robots
{
    public static class ParamGuard
    {
        public static void NotNull(object param, string paramName)
        {
            if (param == null)
                throw new ArgumentNullException(message: "Parameter can not be null.", paramName: paramName);
        }

        public static void SupportedFormat(string param, string paramName, Predicate<string> predicate)
        {
            if (!predicate(param))
                throw new ArgumentException(message: "Unsupported format.", paramName: paramName);
        }

        public static Exception NotSupposedToGetHere { get; } = new InvalidOperationException("Not supposed to get here");
    }
}