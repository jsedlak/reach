﻿using Reach.Content.Events.Fields;

namespace Reach.Content.Model;

/// <summary>
/// Describes a type of field that can be used in the platform. For example
/// fields may include a Text Field, Image, Link, or more.
/// </summary>
public class FieldDefinition
{
    public void Apply(FieldDefinitionCreatedEvent @event)
    {
        Id = @event.AggregateId;
        TenantId = @event.TenantId;
        Name = @event.Name;
        Key = @event.Key;
        EditorDefinitionId = @event.EditorDefinitionId;
        EditorParameters = @event.EditorParameters;
    }

    public void Apply(FieldDefinitionEditorSetEvent @event)
    {
        EditorDefinitionId = @event.EditorDefinitionId;
    }

    public void Apply(FieldDefinitionEditorParametersSetEvent @event)
    {
        EditorParameters = @event.EditorParameters;
    }

    public void Apply(FieldDefinitionDeletedEvent @event)
    {
        IsDeleted = true;
    }

    /// <summary>
    /// Gets or Sets the unique identifier for this definition
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or Sets the identifier of the tenant to which this definition belongs
    /// </summary>
    public Guid TenantId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or Sets the display name for this field definition
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or Sets a slug version of the name
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the identifier for the data that defines the editor for managing this field's data
    /// </summary>
    public Guid EditorDefinitionId { get; set; }

    /// <summary>
    /// Gets or Sets the set of parameters and values to pass to the editor component
    /// </summary>
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();

    /// <summary>
    /// Gets or Sets whether this definition has been deleted
    /// </summary>
    public bool IsDeleted { get; set; }
}
