using System;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace FlightSimulator
{
    class UnfitForFlightException : Exception
    {
        public UnfitForFlightException()
        {
        }

        public UnfitForFlightException(string message) : base(message)
        {
        }

        public UnfitForFlightException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnfitForFlightException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
