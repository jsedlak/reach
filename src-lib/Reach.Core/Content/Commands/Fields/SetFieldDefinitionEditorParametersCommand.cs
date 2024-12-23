﻿using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class SetFieldDefinitionEditorParametersCommand : AggregateCommand
{
    public SetFieldDefinitionEditorParametersCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}