using Microsoft.Extensions.Logging;
using Reach.Content.Events.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Grains.ComponentDefinitions;

[ImplicitStreamSubscription(GrainConstants.ComponentDefinition_EventStream)]
public class ComponentDefinitionViewGrain : SubscribedViewGrain<BaseComponentDefinitionEvent>,
    IComponentDefinitionViewGrain
{
    private readonly IComponentDefinitionViewReadRepository _componentDefinitionViewReadRepository;
    private readonly IComponentDefinitionViewWriteRepository _componentDefinitionViewWriteRepository;

    public ComponentDefinitionViewGrain(
        IComponentDefinitionViewReadRepository componentDefinitionViewReadRepository,
        IComponentDefinitionViewWriteRepository componentDefinitionViewWriteRepository,
        ILogger<ComponentDefinitionViewGrain> logger)
        : base(logger)
    {
        _componentDefinitionViewReadRepository = componentDefinitionViewReadRepository;
        _componentDefinitionViewWriteRepository = componentDefinitionViewWriteRepository;
    }

    public async Task Handle(ComponentDefinitionCreatedEvent @event)
    {
        var result = await _componentDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        if (result is null)
        {
            result = new ComponentDefinitionView
            {
                OrganizationId = @event.OrganizationId,
                HubId = @event.HubId,
                Id = @event.AggregateId
            };
        }
        
        result.Name = @event.Name;
        result.Slug = @event.Slug;

        await _componentDefinitionViewWriteRepository.Upsert(result);
    }

    public async Task Handle(ComponentDefinitionRenamedEvent @event)
    {
        var result = await _componentDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );
        
        result!.Name = @event.Name;
        result!.Slug = @event.Slug;

        await _componentDefinitionViewWriteRepository.Upsert(result);
    }

    public Task Handle(ComponentDefinitionRendererSetEvent @event)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(FieldAddedToComponentDefinitionEvent @event)
    {
        var result = await _componentDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result!.Fields =
        [
            ..result.Fields,
            @event.Field
        ];

        await _componentDefinitionViewWriteRepository.Upsert(result);
    }

    public async Task Handle(FieldRemovedFromComponentDefinitionEvent @event)
    {
        var result = await _componentDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result!.Fields = result.Fields
            .Where(m => m.Id != @event.FieldId)
            .ToArray();

        await _componentDefinitionViewWriteRepository.Upsert(result);
    }

    public async Task Handle(ComponentDefinitionDeletedEvent @event)
    {
        var result = await _componentDefinitionViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        await _componentDefinitionViewWriteRepository.Delete(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );
    }
}