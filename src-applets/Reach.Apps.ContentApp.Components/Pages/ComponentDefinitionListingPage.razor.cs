using Microsoft.AspNetCore.Components;
using Reach.Apps.ContentApp.Services;
using Reach.Components.Context;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

public partial class ComponentDefinitionListingPage : ContentBasePage
{
    private readonly ComponentDefinitionService _componentDefinitionService;
    private IEnumerable<ComponentDefinitionView> _componentDefinitions;

    private DialogContext<CreateComponentDefinitionCommand> _createContext = new(() => { });

    public ComponentDefinitionListingPage(ComponentDefinitionService componentDefinitionService)
    {
        _componentDefinitionService = componentDefinitionService;
        
        _createContext = new(StateHasChanged);
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
        _componentDefinitions = await _componentDefinitionService.GetComponentDefinitions();
    }
    
    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new (Guid.Empty, Guid.Empty, Guid.NewGuid()));
    }
    
    private Task OnCancelCreateClicked()
    {
        return _createContext.Cancel();
    }

    private async Task OnConfirmCreateClicked()
    {
        await _createContext.Confirm(async (model) =>
        {
            if(model == null)
            {
                return (false, ["Invalid data."]);
            }

            var result = await _componentDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }
    
    [CascadingParameter]
    public TenantContext TenantContext { get; set; } = null!;
}