using System;
using System.Diagnostics;

namespace Reach.Diagnostics
{
    public sealed class TraceLog : ILog
    {
        public void Error(string message, Exception ex = null)
        {
            Trace.TraceError(message);
        }

        public void Event(string message, object data = null)
        {
            Trace.TraceInformation(message);
        }

        public void Info(string message, object data = null)
        {
            Trace.TraceInformation(message);
        }

        public void Metric(string name, double value)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, object data = null)
        {
            Trace.TraceInformation(message);
        }
    }
}
