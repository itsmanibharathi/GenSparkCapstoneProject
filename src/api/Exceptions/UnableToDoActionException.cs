using System.Runtime.Serialization;

namespace api.Exceptions
{
    [Serializable]
    public class UnableToDoActionException : Exception
    {
        public UnableToDoActionException()
        {
        }

        public UnableToDoActionException(string? message) : base(message)
        {
        }

        public UnableToDoActionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToDoActionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}