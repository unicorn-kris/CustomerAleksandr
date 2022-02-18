using System;
using System.Runtime.Serialization;

namespace Repository.Exceptions
{
    public class ProductRepositoryException : Exception
    {
        public ProductRepositoryException()
        {
        }

        public ProductRepositoryException(string message)
            : base(message)
        {
        }

        public ProductRepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProductRepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
