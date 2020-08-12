using System;
using System.Collections.Generic;

namespace Reach.DataModel
{
    public abstract class Event : Entity
    {
        public Event()
        {
        }

        public Event(Guid resourceId)
        {
            ResourceId = resourceId;
        }

        public Dictionary<string, Guid> RelatedResources { get; set; } = new Dictionary<string, Guid>();

        public Guid ResourceId { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
