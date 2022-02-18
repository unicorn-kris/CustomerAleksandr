using System;
using System.Runtime.Serialization;

namespace Repository.Exceptions
{
    internal class UserRepositoryException : Exception
    {
        public UserRepositoryException()
        {
        }

        public UserRepositoryException(string message)
            : base(message)
        {
        }

        public UserRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
