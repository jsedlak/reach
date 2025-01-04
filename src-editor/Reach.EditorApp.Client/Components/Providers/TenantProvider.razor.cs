using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    private PersistingComponentStateSubscription subscription;

    public TenantProvider(
        PersistentComponentState applicationState, 
        NavigationManager navigationManager,
        IOrganizationService organizationService,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _applicationState = applicationState;
        _navigationManager = navigationManager;
        _navigationManager.LocationChanged += OnLocationChanged;
        _organizationService = organizationService;
        _authenticationStateProvider = authenticationStateProvider;
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
            Console.WriteLine("Calling for organizations and hubs.");

            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

            if(state.User.Identity == null || 
               state.User.Identity.Name == null ||
               !state.User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Cannot request orgs because Authentication State is null.");
                return;
            }

            // TODO: Need to query for current org, or do we ?
            var orgs = await _organizationService.GetOrganizationsForUserAsync(
                state.User.Identity.Name
            );
            
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
            return TenantContext.AvailableHubs.FirstOrDefault(m => m.Slug == pathSplit[2]);
        }

        return null;
    }

    void IDisposable.Dispose()
    {
        subscription.Dispose();

        _navigationManager.LocationChanged -= OnLocationChanged;
    }

    [CascadingParameter]
    public AuthenticationState AuthenticationState { get; set; }

    [CascadingParameter]
    public TenantContext TenantContext { get; set; } = null!;
}
