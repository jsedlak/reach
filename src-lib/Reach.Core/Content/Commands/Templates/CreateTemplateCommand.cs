using Reach.Cqrs;

namespace Reach.Content.Commands.Templates;

/// <summary>
/// Creates a new template item in the platform
/// </summary>
public class CreateTemplateCommand : AggregateCommand
{
    public CreateTemplateCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public CreateTemplateCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    /// <summary>
    /// Gets or Sets the name of the new template
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
