using System;
using System.Runtime.Serialization;

namespace Logic.Exceptions
{
    public class UserLogicException : Exception
    {
        public UserLogicException()
        {
        }

        public UserLogicException(string message)
            : base(message)
        {
        }

        public UserLogicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserLogicException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
