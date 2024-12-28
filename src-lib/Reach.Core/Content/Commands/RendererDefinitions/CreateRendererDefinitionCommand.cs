using Reach.Cqrs;

namespace Reach.Content.Commands.RendererDefinitions;

[GenerateSerializer]
public class CreateRendererDefinitionCommand : AggregateCommand
{
    public CreateRendererDefinitionCommand()
        : base(Guid.Empty, Guid.NewGuid(), Guid.Empty)
    {

    }

    public CreateRendererDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId) 
        : base(organizationId, aggregateId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}
