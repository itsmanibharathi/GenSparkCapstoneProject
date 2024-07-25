using System.Runtime.Serialization;

namespace api.Exceptions
{
    [Serializable]
    internal class InvalidUserCredentialException : Exception
    {
        public InvalidUserCredentialException()
        {
        }

        public InvalidUserCredentialException(string? message) : base(message)
        {
        }

        public InvalidUserCredentialException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUserCredentialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}