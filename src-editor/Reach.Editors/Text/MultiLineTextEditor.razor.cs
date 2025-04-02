using Microsoft.AspNetCore.Components;

namespace Reach.Editors.Text;

public partial class MultiLineTextEditor : BaseEditor
{
    private Task OnTextAreaChanged(ChangeEventArgs args)
    {
        return OnValueChanged(args.Value?.ToString() ?? string.Empty);
    }
}
