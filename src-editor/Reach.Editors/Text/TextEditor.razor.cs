using Microsoft.AspNetCore.Components;

namespace Reach.Editors.Text;

public partial class TextEditor : BaseEditor
{
    [Parameter]
    public int? MaxLength { get; set; }
}
