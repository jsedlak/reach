using Reach.Content.Model;

namespace Reach.Content.Events.EditorDefinitions;

[GenerateSerializer]
public class ParameterAddedToEditorDefinitionEvent : BaseEditorDefinitionEvent
{
    public ParameterAddedToEditorDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {

    }

    [Id(0)]
    public string DisplayName { get; set; }

    [Id(1)]
    public EditorParameterType Type { get; set; }
}