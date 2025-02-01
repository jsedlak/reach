using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Layout;

public abstract class AuthenticatedLayoutBase : LayoutComponentBase
{
    private IEnumerable<AvailableOrganizationView>? _organizations = null;

    private readonly NavigationManager _navigation;
    private readonly IMembershipService _membershipService;
    private readonly IOrganizationService _organizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    
    private Guid? _currentOrganizationId = Guid.Empty;
    private Guid? _currentHubId = Guid.Empty;

    private bool _isLoading = false;
    private bool _isReady = false;

    public AuthenticatedLayoutBase(
        NavigationManager navigation, 
        IMembershipService membershipService, 
        IOrganizationService organizationService, 
        AuthenticationStateProvider authenticationStateProvider)
    {
        _navigation = navigation;
        _navigation.LocationChanged += OnLocationChanged;
        _membershipService = membershipService;
        _organizationService = organizationService;
        _authenticationStateProvider = authenticationStateProvider;
        _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
    }

    private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        Console.WriteLine(nameof(OnAuthenticationStateChanged));

        var state = await task;
        if(state.User?.Identity is { IsAuthenticated: true })
        {
            await InitializeOrganizations();
        }
    }

    private void OnLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        Console.WriteLine("OnLocationChanged");
        RefreshOrgHubView();
        StateHasChanged();
    }

    private async Task InitializeOrganizations()
    {
        Console.WriteLine("Initializing organizations...");
        
        _isLoading = true;
        _isReady = false;
        StateHasChanged();

        var settings = await _membershipService.GetAccountSettings();
        _organizations = await _organizationService.GetAvailableOrganizations();

        var uri = _navigation.ToAbsoluteUri(_navigation.Uri);
        var queryStringParams = QueryHelpers.ParseQuery(uri.Query);

        var skip = settings.SkipsOnboarding ||
                   queryStringParams.ContainsKey("skipOnboarding");

        _isLoading = false;
        StateHasChanged();
        
        Console.WriteLine($"Loaded {_organizations?.Count() ?? 0} organizations.");

        if (!_organizations.Any())
        {
            if (settings.SkipsOnboarding || skip)
            {
                _isReady = true;
                StateHasChanged();
                
                return;
            }

            _navigation.NavigateTo("/onboarding");
        }
        else
        {
            await RefreshOrgHubView();

            _isReady = true;
            StateHasChanged();
        }
    }

    private async Task RefreshOrgHubView()
    {
        if (_organizations == null || _organizations.Count() == 0)
        {
            _currentHubId = null;
            _currentOrganizationId = null;
            return;
        }
        
        var uri = _navigation.ToAbsoluteUri(_navigation.Uri);
        var parts = uri.PathAndQuery.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length > 2 && parts.First().Equals("app", StringComparison.OrdinalIgnoreCase))
        {
            var orgSlug = parts[1];
            var hubSlug = parts[2];

            var currentOrg = _organizations.FirstOrDefault(x => x.Slug == orgSlug);

            if (currentOrg is null)
            {
                return;
            }
            
            _currentOrganizationId = currentOrg.Id;
            _currentHubId = currentOrg.Hubs.FirstOrDefault(m => m.Slug == hubSlug)?.Id;

            await OnPageReady();
        }
    }

    protected virtual Task OnPageReady()
    {
        return Task.CompletedTask;
    }

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("GlobalLayout::OnInitializedAsync");

        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        Console.WriteLine($"Current user: {state.User?.Identity?.Name}");
        
        if (state is null || state.User?.Identity is not {  IsAuthenticated: true })
        {
            Console.WriteLine("Auth state is null. Waiting.");
            return;
        }

        Console.WriteLine("OnInitializedAsync -> Initializing Orgs");
        await InitializeOrganizations();
    }
    
    protected Guid? CurrentOrganizationId => _currentOrganizationId;
    
    protected Guid? CurrentHubId => _currentHubId;
    
    protected NavigationManager Navigation => _navigation;
    
    protected bool IsLoading => _isLoading;
    
    protected bool IsReady => _isReady;
    
    protected IEnumerable<AvailableOrganizationView>? Organizations => _organizations;
}