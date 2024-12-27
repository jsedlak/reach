using Reach.Cqrs;

namespace Reach.Content.Commands.Templates;

/// <summary>
/// Creates a new template item in the platform
/// </summary>
public class CreateTemplateCommand : AggregateCommand
{
    public CreateTemplateCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }

    /// <summary>
    /// Gets or Sets the name of the new template
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
