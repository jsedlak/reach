using Reach.Cqrs;

namespace Reach.Content.Commands.Templates;

/// <summary>
/// Creates a new template item in the platform
/// </summary>
public class CreateTemplateCommand : AggregateRootCommand
{
    public CreateTemplateCommand(Guid aggregateRootId) 
        : base(aggregateRootId)
    {
    }

    /// <summary>
    /// Gets or Sets the name of the new template
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
