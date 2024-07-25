using System.Runtime.Serialization;

namespace api.Exceptions
{
    [Serializable]
    internal class UserNotActivateException : Exception
    {
        public UserNotActivateException()
        {
        }

        public UserNotActivateException(string? message) : base(message)
        {
        }

        public UserNotActivateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotActivateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}