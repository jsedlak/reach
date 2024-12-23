﻿using Reach.Content.Commands.Fields;
using Reach.Content.Events.Fields;
using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public class FieldDefinitionGrain : StreamingEventSourcedGrain<FieldDefinition, BaseFieldDefinitionEvent>, IFieldDefinitionGrain
{
    public FieldDefinitionGrain() 
        : base(GrainConstants.FieldDefinition_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        await Raise(new FieldDefinitionCreatedEvent(command.AggregateId, command.TenantId)
        {
            Name = command.Name,
            Key = command.Name.ToSlug(),
            EditorDefinitionId = command.EditorDefinitionId,
            EditorParameters = Array.Empty<EditorParameter>()
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command)
    {
        await Raise(new FieldDefinitionEditorSetEvent(command.AggregateId, command.TenantId)
        {
            EditorDefinitionId = command.EditorDefinitionId,
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command)
    {
        await Raise(new FieldDefinitionEditorParametersSetEvent(command.AggregateId, command.TenantId)
        {
            EditorParameters = command.EditorParameters
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command)
    {
        await Raise(new FieldDefinitionDeletedEvent(command.AggregateId, command.TenantId));

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }
}
