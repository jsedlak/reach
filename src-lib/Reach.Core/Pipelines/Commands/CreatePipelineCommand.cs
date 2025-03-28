﻿using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class CreatePipelineCommand : AggregateCommand
{
    public CreatePipelineCommand(ResourceId aggregateId) 
        : base(aggregateId)
    {
    }

    public CreatePipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
