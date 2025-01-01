using Reach.Cqrs;

namespace Reach.Content.Commands.Content;

[GenerateSerializer]
public class CreateContentCommand : AggregateCommand
{
    public CreateContentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public CreateContentCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    /// <summary>
    /// Gets or Sets a friendly display name for the content
    /// </summary>
    [Id(0)] public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or Sets a source friendly key for the component 
    /// </summary>
    [Id(1)] public string Slug { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or Sets the component definition by which this content is templated
    /// </summary>
    [Id(2)] public Guid ComponentDefinitionId { get; set; } = Guid.Empty;
}