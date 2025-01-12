using Microsoft.AspNetCore.Components;
using Reach.Membership.ApiModel;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Pages;

public partial class Onboarding : ComponentBase
{
    private readonly NavigationManager _navigationManager;
    private readonly IMembershipService _membershipService;
    private readonly IOrganizationService _organizationService;

    private bool _isProcessing = false;
    private CreateOrganizationRequest _model = new();
    private string? _errorMessage = null;

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
}
