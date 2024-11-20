using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Reach.Apps.ContentApp.Services;
using Reach.Components.Context;
using Reach.Content.Commands.Editors;
using Reach.Content.Views;
using Reach.Membership.Views;
using Reach.Security;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class EditorDefinitionListingPage : ContentBasePage
{
    private readonly ITenantContext? _tenantContext;

    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];

    private DialogContext<CreateEditorDefinitionCommand> _createContext = new(() => { });

    private AvailableTenantView? _tenant;

    public EditorDefinitionListingPage(EditorDefinitionService editorDefinitionService, IServiceProvider serviceProvider)
    {
        EditorDefinitionService = editorDefinitionService;

        _createContext = new(StateHasChanged);

        _tenantContext = serviceProvider.GetRequiredService<ITenantContext>();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await RefreshData();
            StateHasChanged();
        }
    }

    private async Task RefreshData()
    {
        _editorDefinitions = await EditorDefinitionService.GetEditorDefinitions();
        _tenant = await _tenantContext.GetCurrentTenant();
    }

    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new CreateEditorDefinitionCommand(Guid.NewGuid()));
    }

    private Task OnCancelCreateClicked()
    {
        return _createContext.Cancel();
    }

    private async Task OnConfirmCreateClicked()
    {
        await _createContext.Confirm(async (model) =>
        {
            var result = await EditorDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }

    private EditorDefinitionService EditorDefinitionService { get; }
}
