using Microsoft.AspNetCore.Components;
using Reach.Content.Model;
using Reach.Content.Views;
using Reach.Platform.Providers;
using Tazor.Components;

namespace Reach.Apps.ContentApp.Components.Controls;

public partial class ComponentFieldEditor : TazorBaseComponent
{
    private readonly IContentContextProvider _contentContextProvider;

    private FieldDefinitionView _fieldDefn = new();
    private EditorDefinitionView _editorDefn = new();

    private string _value = "";

    public ComponentFieldEditor(IContentContextProvider contentContextProvider)
    {
        _contentContextProvider = contentContextProvider;
    }

    protected override void OnInitialized()
    {
        if(Field is null)
        {
            return;
        }

        _value = Field.Value;

        _fieldDefn = _contentContextProvider.FieldDefinitions
            .First(m => m.Id == Field.DefinitionId);

        _editorDefn = _contentContextProvider.EditorDefinitions
            .First(m => m.Id == _fieldDefn.EditorDefinitionId);
    }

    private async Task OnValueChanged(string newValue)
    {
        _value = newValue;

        if(ValueChanged is {  HasDelegate: true })
        {
            await ValueChanged.InvokeAsync(_value);
        }
    }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    [Parameter]
    public Field Field { get; set; }
}
