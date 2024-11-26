using Microsoft.AspNetCore.Components;
using Reach.Components.Context;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Client.Components.Layout.Tenants;

public partial class TenantDropdown : ComponentBase
{
    private readonly NavigationManager _navigationManager;
    private readonly IRegionUrlFormatter _regionUrlFormatter;

    private bool _isOpen = false;

    public TenantDropdown(IRegionUrlFormatter regionUrlFormatter, NavigationManager navigationManager)
    {
        _regionUrlFormatter = regionUrlFormatter;
        _navigationManager = navigationManager;
    }

    protected override void OnInitialized()
    {
        TenantContext.PropertyChanged += OnTenantContextChanged;
    }

    private void OnTenantContextChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        StateHasChanged();
    }

    private void OnClickedOutside()
    {
        _isOpen = false;
        StateHasChanged();
    }

    private void OnDropdownClicked()
    {
        _isOpen = !_isOpen;
        StateHasChanged();
    }

    //private Task SelectTenant(AvailableTenantView tenant)
    //{
    //    _currentTenant = tenant;
    //    _isOpen = false;
    //    StateHasChanged();
    //    return Task.CompletedTask;
    //}

    [CascadingParameter]
    private TenantContext TenantContext { get; set; } = null!;
}
