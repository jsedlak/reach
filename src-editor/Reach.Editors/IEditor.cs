using Microsoft.AspNetCore.Components;

namespace Reach.Editors;

public interface IEditor
{
    Task SetValueAsync(string value);

    EventCallback<string> ValueChanged { get; set; }
}