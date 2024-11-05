﻿namespace Reach.Content.Model;

public class ComponentDefinition
{
    public Guid Id { get; set; } = new Guid();

    public string Name { get; set; } = "Untitled Component";

    public Guid ParentId { get; set; } = Guid.Empty;

    public FieldDefinition[] Fields { get; set; } = Array.Empty<FieldDefinition>();
}