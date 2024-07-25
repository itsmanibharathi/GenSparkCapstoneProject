using System.Runtime.Serialization;

namespace api.Exceptions
{
    [Serializable]
    public class EnvironmentVariableUndefinedException : Exception
    {
        public EnvironmentVariableUndefinedException()
        {
        }

        public EnvironmentVariableUndefinedException(string? message) : base(message)
        {
        }

        public EnvironmentVariableUndefinedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EnvironmentVariableUndefinedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}