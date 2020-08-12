using System;

namespace Reach.Diagnostics
{
    public interface ILog
    {
        void Info(string message, object data = null);

        void Warn(string message, object data = null);

        void Error(string message, Exception ex = null);

        void Event(string message, object data = null);

        void Metric(string name, double value);
    }
}
