﻿using HotChocolate;
using HotChocolate.Types;
using Reach.Content.ServiceModel;
using Reach.Content.Views;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class EditorDefinitionQueries
{
    public Task<IQueryable<EditorDefinitionView>> EditorDefinitions([Service] IEditorDefinitionViewReadRepository repository) =>
        repository.Query();
}
