using Reach.Content.Commands.EditorDefinitions;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Membership.GrainModel;

namespace Reach.Silo.Membership.Grains;

internal class HubManagementGrain : Grain, IHubManagementGrain
{
    private (Guid organizationId, Guid hubId) GetIds()
    {
        var keys = this.GetPrimaryKeyString()
            .Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        return (
            Guid.Parse(keys[0]),
            Guid.Parse(keys[1])
        );  
    }

    public async Task<CommandResponse> Initialize()
    {
        var identifier = GetIds();
        var aggId = new AggregateId(identifier.organizationId, identifier.hubId, Guid.NewGuid());

        var editorDefinition = this.GrainFactory.GetGrain<IEditorDefinitionGrain>(aggId);

        await editorDefinition.Create(new CreateEditorDefinitionCommand(
            aggId.OrganizationId, aggId.HubId, aggId.ResourceId)
        {
            Name = "TextBox",
            Slug = "textbox",
            EditorType = "Reach.Editors.Text.TextEditor"
        });

        return CommandResponse.Success();
    }

    public Task<CommandResponse> Upgrade()
    {
        return Task.FromResult(
            CommandResponse.Success()
        );
    }
}