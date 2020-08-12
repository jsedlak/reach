using System;
using System.Collections.Generic;
using System.Text;

namespace Reach.ServiceModel
{
    public class ServiceResult
    {
        public bool IsSuccessful { get; set; }

        public IEnumerable<ServiceMessage> Messages { get; set; }

        /// <summary>
        /// Gets or Sets the data being returned as a result of the pipeline
        /// </summary>
        public object Data { get; set; }
    }
}
