using Microsoft.AspNetCore.Components;
using Reach.Membership.ApiModel;
using Reach.Platform.ServiceModel;
using Tazor.Components.Theming;

namespace Reach.Platform.Pages;

public partial class Onboarding : ComponentBase
{
    private readonly NavigationManager _navigationManager;
    private readonly IMembershipService _membershipService;
    private readonly IOrganizationService _organizationService;

    private bool _isProcessing = false;
    private CreateOrganizationRequest _model = new();
    private string? _errorMessage = null;

    private string[] _hubIconUrls = [
        "/img/hub-icons/sun.jpg",
        "/img/hub-icons/cat.jpg",
        "/img/hub-icons/gsp-dog.jpg",
        "/img/hub-icons/rocket.jpg"
    ];

    public Onboarding(NavigationManager navigationManager, IMembershipService membershipService, IOrganizationService organizationService)
    {
        _membershipService = membershipService;
        _navigationManager = navigationManager;
        _organizationService = organizationService;
    }

    private async Task OnSkipClicked()
    {
        await _membershipService.SetSkipOnboardingFlag(new Membership.Commands.SetSkipOnboardingFlagCommand
        {
            SkipOnboarding = true
        });

        _navigationManager.NavigateTo("/?skipOnboarding=true");
    }

    private async Task BeginCreate()
    {
        _errorMessage = null;
        _isProcessing = true;
        StateHasChanged();

        var result = await _organizationService.Onboard(_model);
        
        if (result.IsSuccess)
        {
            _navigationManager.NavigateTo("/");
        }
        else
        {
            _errorMessage = "Could not create the organization!";
        }

        _isProcessing = false;
        StateHasChanged();
    }

    private Task OnOrganizationChanged(string value)
    {
        _model.OrganizationName = value ?? ""; 
        _model.OrganizationSlug = _model.OrganizationName.Slugify();
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnHubChanged(string value)
    {
        _model.HubName = value ?? "";
        _model.HubSlug = _model.HubName.Slugify();
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets or Sets the theme that has been cascaded to this component
    /// </summary>
    [CascadingParameter(Name = "Theme")]
    public ITheme Theme { get; set; } = null!;
}
