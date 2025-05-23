﻿using Reach.Content.Commands.FieldDefinitions;
using Reach.Cqrs;

namespace Reach.Silo.Content.GrainModel;

public interface IFieldDefinitionGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateFieldDefinitionCommand command);

    Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command);

    Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command);

    Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command);
}