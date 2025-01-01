using Reach.Content.Events.ComponentDefinitions;
using Reach.Content.Model;

namespace Reach.Silo.Content.Model;

/// <summary>
/// Describes a template for a <see cref="Component"/>
/// </summary>
public class ComponentDefinition
{
    public void Apply(ComponentDefinitionCreatedEvent @event)
    {
        OrganizationId = @event.OrganizationId;
        HubId = @event.HubId;
        Id = @event.AggregateId;
        Name = @event.Name;
        Slug = @event.Slug;
        ParentId = @event.ParentId;
    }

    public void Apply(ComponentDefinitionRenamedEvent @event)
    {
        Name = @event.Name;
        Slug = @event.Slug;
    }

    public void Apply(ComponentDefinitionDeletedEvent @event)
    {
        IsDeleted = true;
    }

    public void Apply(FieldAddedToComponentDefinition @event)
    {
        Fields =
        [
            ..Fields,
            @event.Field
        ];
    }

    public void Apply(FieldRemovedFromComponentDefinitionEvent @event)
    {
        Fields = Fields.Where(m => m.Id != @event.FieldId).ToArray();
    }
    
    /// <summary>
    /// Gets or Sets the identifier of the organization to which this definition belongs
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or Sets the identifier of the hub to which this definition belongs
    /// </summary>
    public Guid HubId { get; set; }

    /// <summary>
    /// Gets or Sets the unique identifier of this component definition
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or Sets the name of this component definition
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the code friendly name of this component definition
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the parent of this component definition
    /// </summary>
    public Guid ParentId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or Sets the fields associated with this component definition
    /// </summary>
    public Field[] Fields { get; set; } = Array.Empty<Field>();

    /// <summary>
    /// Gets or Sets whether or not the component definition is considered deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}