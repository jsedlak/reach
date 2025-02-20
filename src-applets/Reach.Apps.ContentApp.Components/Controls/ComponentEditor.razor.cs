using Microsoft.AspNetCore.Components;
using Reach.Content.Commands.Components;
using Reach.Content.Model;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.Providers;
using Reach.Platform.ServiceModel;
using Tazor.Components;

namespace Reach.Apps.ContentApp.Components.Controls;

public partial class ComponentEditor : TazorBaseComponent
{
    private readonly IContentContextProvider _contentContextProvider;
    private readonly IComponentService _componentService;

    private bool _isChanged;
    private Dictionary<string, string> _changedFields = new();

    public ComponentEditor(
        IContentContextProvider contentContextProvider,
        IComponentService componentService)
    {
        _componentService = componentService;
        _contentContextProvider = contentContextProvider;
    }

    protected async Task OnSaveClicked()
    {
        if (Component is null)
        {
            // TODO: How is this possible lol. Can't save a change
            // to a field on a component that is null
            return;
        }

        var aggId = new AggregateId(
            Component.OrganizationId,
            Component.HubId,
            Component.Id
        );

        // TODO: batch these 
        // TODO: figure out how to return when errors happen how to display them and keep the new value
        foreach (var fieldChange in _changedFields)
        {
            await _componentService.SetFieldValue(new SetComponentFieldValueCommand(aggId)
            {
                FieldKey = fieldChange.Key,
                Value = fieldChange.Value
            });
        }

        _changedFields.Clear();

        StateHasChanged();
    }

    private async Task OnFieldValueChanged(Field changedField, string value)
    {
        if (value == changedField.Value)
        {
            if (_changedFields.ContainsKey(changedField.Slug))
            {
                _changedFields.Remove(changedField.Slug);
            }
        }
        else
        {
            _changedFields[changedField.Slug] = value;
        }

        _isChanged = _changedFields.Count > 0;
    }

    [Parameter]
    public ComponentView? Component { get; set; }

}
