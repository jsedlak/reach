using Microsoft.AspNetCore.Components;

namespace Reach.Elements.ServiceModel
{
    public interface IIconLibrary
    {
        MarkupString GetIcon(string iconName);
    }
}
