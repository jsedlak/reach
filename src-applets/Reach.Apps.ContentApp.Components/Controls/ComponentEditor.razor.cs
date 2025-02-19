using Microsoft.AspNetCore.Components;
using Reach.Content.Commands.Components;
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

    private IEnumerable<FieldEditorMap> _editorMaps = [];

    private ComponentView? _lastComponent;
    private DateTimeOffset _updatedAt = DateTime.UtcNow;

    public ComponentEditor(
        IContentContextProvider contentContextProvider,
        IComponentService componentService)
    {
        _componentService = componentService;
        _contentContextProvider = contentContextProvider;
    }

    protected async Task OnSaveClicked()
    {
        if(Component is null)
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
        foreach (var field in _editorMaps)
        {
            if(!field.IsChanged)
            {
                continue;
            }

            await _componentService.SetFieldValue(new SetComponentFieldValueCommand(aggId)
            {
                Value = field.Value
            });
        }
    }

    protected override void OnInitialized()
    {
        _editorMaps = [];

        if (Component is null || !Component.Fields.Any())
        {
            return;
        }

        Console.WriteLine("OnInitialized");

        if (_lastComponent is null || _lastComponent.Id != Component.Id)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        Console.WriteLine("Initialize");

        _editorMaps = Component.Fields.Select(field =>
        {
            var fieldDefn = _contentContextProvider.FieldDefinitions
                .First(m => m.Id == field.DefinitionId);

            var editorDefn = _contentContextProvider.EditorDefinitions
                .First(m => m.Id == fieldDefn.EditorDefinitionId);

            var fieldMap = new FieldEditorMap
            {
                Name = field.Name,
                Slug = field.Slug,
                StateHasChanged = StateHasChanged,
                FieldId = field.Id,
                OriginalValue = field.Value,
                Value = field.Value,
                EditorType = Type.GetType(editorDefn.EditorType)!
            };

            var parameters = new Dictionary<string, object>();

            parameters.Add("Value", fieldMap.Value);
            parameters.Add(
                "ValueChanged",
                EventCallback.Factory.Create<string>(
                    fieldMap,
                    async (string newValue) =>
                    {
                        _updatedAt = DateTimeOffset.UtcNow;
                        await fieldMap.OnChange(newValue);
                    }
                )
            );

            // TODO: support adding our custom field->editor definition parameters
            // A string value will not always be valid, so we'll need to look up
            // the parameter defn and convert the value
            //foreach(var parm in fieldDefn.EditorParameters)
            //{
            //    parameters.Add(parm.Key, parm.Value);
            //}

            fieldMap.Parameters = parameters;

            return fieldMap;
        });
    }

    [Parameter]
    public ComponentView? Component { get; set; }

    private class FieldEditorMap
    {
        public Task OnChange(string value)
        {
            Console.WriteLine("Value changed for " + Name + " to " + value);

            Parameters["Value"] = value;
            Value = value;

            Console.WriteLine("New Params value is " + Parameters["Value"]);
            
            StateHasChanged();

            return Task.CompletedTask;
        }

        public Action StateHasChanged { get; set; } = null!;

        public Guid FieldId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string OriginalValue { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public Type EditorType { get; set; } = null!;

        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public bool IsChanged
        {
            get
            {
                return !Value.Equals(OriginalValue, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
