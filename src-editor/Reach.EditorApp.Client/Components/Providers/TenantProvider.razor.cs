using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Reach.Components.Context;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.Views;

namespace Reach.EditorApp.Client.Components.Providers;

public partial class TenantProvider : ComponentBase, IDisposable
{
    public const string PersistentStateKey = "TenantProvider_CurrentTenant";

    private readonly PersistentComponentState _applicationState;
    private readonly NavigationManager _navigationManager;
    private readonly ITenantService _tenantService;

    private PersistingComponentStateSubscription subscription;

    public TenantProvider(
        PersistentComponentState applicationState, 
        NavigationManager navigationManager,
        ITenantService tenantService)
    {
        _applicationState = applicationState;
        _navigationManager = navigationManager;
        _navigationManager.LocationChanged += OnLocationChanged;
        _tenantService = tenantService;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        TenantContext.CurrentTenant = GetCurrentTenant();
    }

    protected override async Task OnInitializedAsync()
    {
        subscription = _applicationState.RegisterOnPersisting(() =>
        {
            _applicationState.PersistAsJson(PersistentStateKey, TenantContext.CurrentTenant);
            return Task.CompletedTask;
        });

        if (!_applicationState.TryTakeFromJson<AvailableTenantView>(PersistentStateKey, out var restored))
        {
            Console.WriteLine("Calling for tenants.");
            TenantContext.AvailableTenants = await _tenantService.GetTenantsForUserAsync();
            TenantContext.CurrentTenant = GetCurrentTenant();
        }
        else
        {
            TenantContext.CurrentTenant = restored!;
        }
    }

    private AvailableTenantView? GetCurrentTenant()
    {
        var path = _navigationManager.ToBaseRelativePath(_navigationManager.Uri).ToLower();

        var pathSplit = path.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        if (pathSplit.Length > 1 && pathSplit[0].Equals("app"))
        {
            return TenantContext.AvailableTenants.FirstOrDefault(m => m.Slug == pathSplit[1]);
        }

        return null;
    }

    void IDisposable.Dispose()
    {
        subscription.Dispose();

        _navigationManager.LocationChanged -= OnLocationChanged;
    }

    [CascadingParameter]
    public TenantContext TenantContext { get; set; } = null!;
}
