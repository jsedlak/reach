using Reach.Content.Events.Components;
using Reach.Content.Model;

namespace Reach.Silo.Content.Model;

public class Component
{
    public void Apply(ComponentCreatedEvent @event)
    {
        OrganizationId = @event.OrganizationId;
        HubId = @event.HubId;
        Name = @event.Name;
        Slug = @event.Slug;
        Fields = @event.Fields;
    }

    public void Apply(ComponentDeletedEvent @event)
    {
        IsDeleted = true;
    }

    public void Apply(ComponentRenamedEvent @event)
    {
        Name = @event.Name;
        Slug = @event.Slug;
    }

    public void Apply(ComponentFieldValueSetEvent @event)
    {
        Fields.First(m => m.Id == @event.FieldId).Value = @event.Value;
    }
    
    public Guid OrganizationId { get; set; }
    
    public Guid HubId { get; set; }
    
    public Guid Id { get; set; }

    public Guid DefinitionId { get; set; }

    public ComponentDefinition Definition { get; set; } = new();

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public Field[] Fields { get; set; } = [];

    public bool IsDeleted { get; set; }
}
