﻿using Reach.Content.Model;

namespace Reach.Silo.Content.Model;

public class ContentItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = "Untitled";

    public Guid ParentId { get; set; } = Guid.Empty;

    public Guid TemplateId { get; set; } = Guid.Empty;

    public Component[] Components { get; set; } = Array.Empty<Component>();

    public Field[] Fields { get; set; } = Array.Empty<Field>();
}
