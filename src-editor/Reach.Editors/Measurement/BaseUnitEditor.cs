using Microsoft.AspNetCore.Components;

namespace Reach.Editors.Measurement;

public abstract class BaseUnitEditor : BaseEditor
{
    [Parameter]
    public string Units { get; set; } = string.Empty;
}
