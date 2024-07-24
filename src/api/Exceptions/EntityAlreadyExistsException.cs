using System.Runtime.Serialization;

namespace api.Exceptions
{
    [Serializable]
    public class EntityAlreadyExistsException<TEntity> : Exception where TEntity : class
    {
        private TEntity? entity;
        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(TEntity entity)
        {
            this.entity = entity;
        }

        public EntityAlreadyExistsException(string? message) : base(message)
        {
        }

        public EntityAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}