using System;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace FlightSimulator
{
    class InsufficientNumberOfDispatchersException : Exception
    {
        public InsufficientNumberOfDispatchersException()
        {
        }

        public InsufficientNumberOfDispatchersException(string message) : base(message)
        {
        }

        public InsufficientNumberOfDispatchersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientNumberOfDispatchersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
