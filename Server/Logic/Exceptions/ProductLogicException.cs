using System;
using System.Runtime.Serialization;

namespace Logic.Exceptions
{
    internal class ProductLogicException : Exception
    {
        public ProductLogicException()
        {
        }

        public ProductLogicException(string message)
            : base(message)
        {
        }

        public ProductLogicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProductLogicException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
