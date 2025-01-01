using Reach.Content.Events.ComponentDefinitions;

namespace Reach.Content.Model;

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

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public Guid ParentId { get; set; } = Guid.Empty;

    public Field[] Fields { get; set; } = Array.Empty<Field>();

    public bool IsDeleted { get; set; }
}