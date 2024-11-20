using Microsoft.AspNetCore.Components.Authorization;
using Reach.Components;
using Reach.Security;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class ContentEditorPage : ContentBasePage
{
    public ContentEditorPage()
    {
    }
}
