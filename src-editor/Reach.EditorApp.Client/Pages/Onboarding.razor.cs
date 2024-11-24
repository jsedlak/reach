using Microsoft.AspNetCore.Components;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.Model;
using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Client.Pages;

public partial class Onboarding : ComponentBase
{
    private readonly ITenantService _tenantService;
    private readonly IRegionService _regionService;
    private readonly NavigationManager _navigation;
    private readonly IRegionUrlFormatter _regionUrlFormatter;

    private IEnumerable<Region> _regions = [];

    private bool _isProcessing = false;
    private Tenant _model = new() { PartitionKey = "", RowKey = "" };
    private string? _errorMessage = null;

    public Onboarding(
        ITenantService tenantService, 
        IRegionService regionService,
        NavigationManager navigationManager,
        IRegionUrlFormatter regionUrlFormatter)
    {
        _tenantService = tenantService;
        _regionService = regionService;
        _navigation = navigationManager;
        _regionUrlFormatter = regionUrlFormatter;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if(firstRender)
        {
            _regions = await _regionService.GetAllRegionsAsync();
            StateHasChanged();
        }
    }

    private async Task BeginCreate()
    {
        _errorMessage = null;
        _isProcessing = true;
        StateHasChanged();

        _model.OwnerIdentifier = "DUMMY";
        var result = await _tenantService.CreateAsync(_model);

        if(result.IsSuccess)
        {
            _navigation.NavigateTo("/");
        }
        else
        {
            _errorMessage = "Could not create the tenant!";
        }

        _isProcessing = false;
        StateHasChanged();
    }
}
