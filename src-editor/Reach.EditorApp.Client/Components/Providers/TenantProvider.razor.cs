using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Reach.Components.Context;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;

namespace Reach.EditorApp.Client.Components.Providers;

public partial class TenantProvider : ComponentBase, IDisposable
{
    public const string PersistentStateKey = "TenantProvider_CurrentHub";

    private readonly PersistentComponentState _applicationState;
    private readonly NavigationManager _navigationManager;
    private readonly IOrganizationService _organizationService;

    private PersistingComponentStateSubscription subscription;

    public TenantProvider(
        PersistentComponentState applicationState, 
        NavigationManager navigationManager,
        IOrganizationService organizationService)
    {
        _applicationState = applicationState;
        _navigationManager = navigationManager;
        _navigationManager.LocationChanged += OnLocationChanged;
        _organizationService = organizationService;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        TenantContext.CurrentHub = GetCurrentHub();
    }

    protected override async Task OnInitializedAsync()
    {
        subscription = _applicationState.RegisterOnPersisting(() =>
        {
            _applicationState.PersistAsJson(PersistentStateKey, TenantContext.CurrentHub);
            return Task.CompletedTask;
        });

        if (!_applicationState.TryTakeFromJson<AvailableHubView>(PersistentStateKey, out var restored))
        {
            Console.WriteLine("Calling for tenants.");
            // TODO: Need to query for current org, or do we ?
            var orgs = await _organizationService.GetOrganizationsForUserAsync();
            TenantContext.AvailableOrganizations = orgs;
            TenantContext.AvailableHubs = orgs.SelectMany(m => m.Hubs);
            TenantContext.CurrentHub = GetCurrentHub();
        }
        else
        {
            TenantContext.CurrentHub = restored!;
        }
    }

    private AvailableHubView? GetCurrentHub()
    {
        var path = _navigationManager.ToBaseRelativePath(_navigationManager.Uri).ToLower();

        var pathSplit = path.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        if (pathSplit.Length > 1 && pathSplit[0].Equals("app"))
        {
            return TenantContext.AvailableHubs.FirstOrDefault(m => m.Slug == pathSplit[1]);
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
