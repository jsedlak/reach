using Microsoft.AspNetCore.Components;
using Reach.Apps.ContentApp.Services;
using Reach.Content.Views;

namespace Reach.Apps.ContentApp.Components.Pages;

public abstract class ContentBasePage : ComponentBase
{
    [Inject]
    protected IServiceProvider ServiceProvider { get; set; } = null!;
}
