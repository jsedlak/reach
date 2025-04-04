﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reach.Content.Events.EditorDefinitions;
using Reach.Silo.Configuration;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Grains.EditorDefinitions;

[ImplicitStreamSubscription(GrainConstants.EditorDefinition_EventStream)]
public class EditorDefinitionViewGrain : SubscribedViewGrain<BaseEditorDefinitionEvent>, IEditorDefinitionViewGrain
{
    private readonly IEditorDefinitionViewReadRepository _editorDefinitionViewReadRepository;
    private readonly IEditorDefinitionViewWriteRepository _editorDefinitionViewWriteRepository;

    public EditorDefinitionViewGrain(
        IEditorDefinitionViewReadRepository editorDefinitionViewReadRepository,
        IEditorDefinitionViewWriteRepository editorDefinitionViewWriteRepository,
        ILogger<EditorDefinitionViewGrain> logger)
        : base(logger)
    {
        _editorDefinitionViewReadRepository = editorDefinitionViewReadRepository;
        _editorDefinitionViewWriteRepository = editorDefinitionViewWriteRepository;
    }

    public async Task Handle(EditorDefinitionCreatedEvent @event)
    {
        var result = await _editorDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        if (result is null)
        {
            result = new Reach.Content.Views.EditorDefinitionView()
            {
                Id = @event.AggregateId,
                OrganizationId = @event.OrganizationId,
                HubId = @event.HubId
            };
        }

        result.Name = @event.Name;
        result.Slug = @event.Slug;
        result.EditorType = @event.EditorType;

        await _editorDefinitionViewWriteRepository.Upsert(result);
    }

    public async Task Handle(EditorDefinitionDeletedEvent @event)
    {
        await _editorDefinitionViewWriteRepository.Delete(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );
    }

    public async Task Handle(EditorDefinitionParametersSetEvent @event)
    {
        var result = await _editorDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        if (result is null)
        {
            throw new InvalidOperationException();
        }

        result.Parameters = @event.Parameters;

        await _editorDefinitionViewWriteRepository.Upsert(result);
    }
}
