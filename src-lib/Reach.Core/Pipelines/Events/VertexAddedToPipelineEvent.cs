﻿namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class VertexAddedToPipelineEvent : BasePipelineEvent
{
    public VertexAddedToPipelineEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid SourceNodeId { get; set; }

    [Id(1)]
    public Guid DestinationNodeId { get; set; }

    [Id(2)]
    public Guid VertexId { get; set; }
}
