using System;
using System.Runtime.Serialization;

namespace Reader.API.Application
{
    [Serializable]
    internal class NoPartAvailableException : Exception
    {
        public NoPartAvailableException()
        {
        }

        public NoPartAvailableException(string message) : base(message)
        {
        }

        public NoPartAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPartAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}