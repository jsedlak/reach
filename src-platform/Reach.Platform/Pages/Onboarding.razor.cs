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

    private bool _orgValid, _hubValid;

    private string[] _hubIconUrls = [
        "/img/hub-icons/sun.jpg",
        "/img/hub-icons/cat.jpg",
        "/img/hub-icons/gsp-dog.jpg",
        "/img/hub-icons/rocket.jpg",
        "/img/hub-icons/sailboat.jpg",
        "/img/hub-icons/ship.jpg",
        "/img/hub-icons/train.jpg",
        "/img/hub-icons/car.jpg"
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

        _model.IconUrl = _hubIconUrls[
            Random.Shared.Next(0, _hubIconUrls.Length - 1)
        ];

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

    private async Task<bool> ValidateOrgCallback(string name, string slug)
    {
        var result = await _organizationService.ValidateOrganizationName(
            new() { Name = name, Slug = slug }
        );

        return result.IsSuccess;
    }

    private Task<bool> ValidateHubCallback(string name, string slug)
    {
        return Task.FromResult(true);
    }

    /// <summary>
    /// Gets or Sets the theme that has been cascaded to this component
    /// </summary>
    [CascadingParameter(Name = "Theme")]
    public ITheme Theme { get; set; } = null!;
}
