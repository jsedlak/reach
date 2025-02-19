using Microsoft.AspNetCore.Components;

namespace Reach.Editors;

public interface IEditor
{
    string Value { get; set; }

    EventCallback<string> ValueChanged { get; set; }
}