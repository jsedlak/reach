using Microsoft.Extensions.Logging;
using Reach.Content.Events.Components;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Grains.Components;

[ImplicitStreamSubscription(GrainConstants.Component_EventStream)]
public class ComponentViewGrain : SubscribedViewGrain<BaseComponentEvent>, IComponentViewGrain
{
    private readonly IComponentViewReadRepository _componentViewReadRepository;
    private readonly IComponentViewWriteRepository _componentViewWriteRepository;

    public ComponentViewGrain(
        IComponentViewReadRepository componentViewReadRepository,
        IComponentViewWriteRepository componentViewWriteRepository,
        ILogger<ComponentViewGrain> logger
    ) : base(logger)
    {
        _componentViewReadRepository = componentViewReadRepository;
        _componentViewWriteRepository = componentViewWriteRepository;
    }

    public async Task Handle(ComponentCreatedEvent @event)
    {
        var result = await _componentViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        if (result is null)
        {
            result = new ComponentView()
            {
                OrganizationId = @event.OrganizationId,
                HubId = @event.HubId,
                Id = @event.AggregateId
            };
        }

        result.Name = @event.Name;
        result.Slug = @event.Slug;
        result.DefinitionId = @event.DefinitionId;
        result.Fields = @event.Fields;

        await _componentViewWriteRepository.Upsert(result);
    }

    public async Task Handle(ComponentRenamedEvent @event)
    {
        var result = await _componentViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result.Name = @event.Name;
        result.Slug = @event.Slug;

        await _componentViewWriteRepository.Upsert(result);
    }

    public async Task Handle(ComponentFieldValueSetEvent @event)
    {
        var result = await _componentViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result!.Fields
            .First(m => m.Id == @event.FieldId)
            .Value = @event.Value;

        await _componentViewWriteRepository.Upsert(result);
    }

    public async Task Handle(ComponentDeletedEvent @event)
    {
        await _componentViewWriteRepository.Delete(@event.OrganizationId, @event.HubId, @event.AggregateId);
    }
}