using System.Runtime.Serialization;

namespace api.Exceptions
{
    [Serializable]
    public class EntityNotFoundException<TEntity> : Exception where TEntity : class
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}