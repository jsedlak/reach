using Microsoft.AspNetCore.Components;

namespace Reach.Editors.Text;

public partial class TextEditor : BaseEditor
{
    private string _value = string.Empty;
    
    protected override Task OnValueSetAsync(string value)
    {
        _value = value;
        return Task.CompletedTask;
    }

    [Parameter]
    public int? MaxLength { get; set; }
}
