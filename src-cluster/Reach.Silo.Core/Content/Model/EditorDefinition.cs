using Reach.Content.Events.EditorDefinitions;
using Reach.Content.Model;

namespace Reach.Silo.Content.Model;

/// <summary>
/// Defines the interface between the content platform and a renderable block of code
/// </summary>
public class EditorDefinition
{
    public void Apply(EditorDefinitionCreatedEvent @event)
    {
        Id = @event.AggregateId;
        OrganizationId = @event.OrganizationId;
        HubId = @event.HubId;
        Name = @event.Name;
        EditorType = @event.EditorType;
    }

    public void Apply(EditorDefinitionParametersSetEvent @event)
    {
        Parameters = @event.Parameters;
    }

    public void Apply(EditorDefinitionDeletedEvent @event)
    {
        IsDeleted = true;
    }

    /// <summary>
    /// Gets or Sets the unique identifier for this editor definition
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or Sets the identifier of the organization to which this definition belongs
    /// </summary>
    public Guid OrganizationId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or Sets the identifier of the hub to which this definition belongs
    /// </summary>
    public Guid HubId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or Sets the display name for this editor definition, shown and used often in the Editor App
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the underlying type information for this editor definition
    /// </summary>
    public string EditorType { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the set of parameters that can be passed to the editor component
    /// </summary>
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();

    /// <summary>
    /// Gets or Sets whether this definition is considered deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
