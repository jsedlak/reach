using Reach.DataModel;
using Reach.ServiceModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Reach.Pipelines.DataModel
{
    /// <summary>
    /// Provides the base implementation of the result of a pipeline execution
    /// </summary>
    public class PipelineResult : ServiceResult
    {
        public static PipelineResult Success(IEnumerable<Event> events, object data = null)
        {
            return new PipelineResult
            {
                IsSuccessful = true,
                Events = events,
                Data = data
            };
        }

        public static PipelineResult Error(short messageCode, string content, MessageSeverityLevel severity = MessageSeverityLevel.Error)
        {
            return Error(new[] { 
                new ServiceMessage
                {
                    Code = messageCode,
                    Content = content,
                    Severity = severity
                } 
            });
        }

        public static PipelineResult Error(IEnumerable<ServiceMessage> messages)
        {
            return new PipelineResult
            {
                IsSuccessful = false,
                Messages = messages
            };
        }

        /// <summary>
        /// Gets or Sets the collection of events that were initiated directly as a result of the execution
        /// </summary>
        public IEnumerable<Event> Events { get; set; }
    }
}
