using Reach.Content.Events.Components;

namespace Reach.Content.Model;

public class Component
{
    public Guid Id { get; set; } = new Guid();

    public Guid DefinitionId { get; set; } = new Guid();

    public ComponentDefinition Definition { get; set; } = new();

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public void Apply(ComponentCreatedEvent @event)
    {
        Name = @event.Name;
        Slug = @event.Slug;
    }

    public void Apply(ComponentDeletedEvent @event)
    {
        
    }

    public void Apply(ComponentRenamedEvent @event)
    {
        Name = @event.Name;
        Slug = @event.Slug;
    }

    public void Apply(ComponentFieldValueSetEvent @event)
    {
        
    }
}
