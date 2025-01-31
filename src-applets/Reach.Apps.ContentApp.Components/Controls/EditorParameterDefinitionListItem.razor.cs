using Microsoft.AspNetCore.Components;
using Reach.Content.Model;
using Tazor.Components;

namespace Reach.Apps.ContentApp.Components.Controls;

public partial class EditorParameterDefinitionListItem : TazorBaseComponent
{
    public event EventHandler? Deleted;

    private bool _isEditing = false;

    private string _name = null!;
    private string _displayName = null!;
    private EditorParameterType _type;

    private void InvokeDeleteParameter()
    {
        Deleted?.Invoke(this, EventArgs.Empty);
    }

    private void BeginEdit()
    {
        _name = Name;
        _displayName = DisplayName;
        _type = Type;

        _isEditing = true;
        StateHasChanged();
    }

    private void CancelEdit()
    {
        _isEditing = false;
        StateHasChanged();
    }

    private async Task CommitEdit()
    {
        Name = _name;
        DisplayName = _displayName;
        Type = _type;

        if(NameChanged.HasDelegate)
        {
            await NameChanged.InvokeAsync(Name);
        }

        if(DisplayNameChanged.HasDelegate)
        {
            await DisplayNameChanged.InvokeAsync(DisplayName);
        }

        if(TypeChanged.HasDelegate)
        {
            await TypeChanged.InvokeAsync(Type);
        }

        _isEditing = false;
        StateHasChanged();
    }

    private void OnNewParameterTypeChanged(ChangeEventArgs e)
    {
        if (e.Value is null)
        {
            return;
        }

        if (Enum.TryParse(typeof(EditorParameterType), e.Value.ToString() ?? "Text", out var type))
        {
            _type = (EditorParameterType)type;
        }
    }

    [Parameter]
    public string Name { get; set; } = null!;

    [Parameter]
    public EventCallback<string> NameChanged { get; set; }

    [Parameter]
    public string DisplayName { get; set; } = null!;

    [Parameter]
    public EventCallback<string> DisplayNameChanged { get; set; }

    [Parameter]
    public EditorParameterType Type { get; set; }

    [Parameter]
    public EventCallback<EditorParameterType> TypeChanged { get; set; }
}
