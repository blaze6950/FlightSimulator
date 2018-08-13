using System;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace FlightSimulator
{
    class PlaneCrashedException : Exception
    {
        public PlaneCrashedException()
        {
        }

        public PlaneCrashedException(string message) : base(message)
        {
        }

        public PlaneCrashedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlaneCrashedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
