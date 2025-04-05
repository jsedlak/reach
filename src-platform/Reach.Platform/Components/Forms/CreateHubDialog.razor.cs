using Microsoft.AspNetCore.Components;
using Reach.Membership.ApiModel;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Components.Forms;

public partial class CreateHubDialog : BaseReachComponent
{
    private string? _errorMessage;
    private bool _hubValid;
    private bool _isProcessing;
    private CreateHubRequest _model = new();

    private readonly IOrganizationService _organizationService;
    private readonly NavigationManager _navigationManager;

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

    public CreateHubDialog(IOrganizationService organizationService, NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _organizationService = organizationService;
    }

    private async Task BeginCreate()
    {
        _errorMessage = null;
        _isProcessing = true;

        StateHasChanged();

        _model.IconUrl = _hubIconUrls[
            Random.Shared.Next(0, _hubIconUrls.Length - 1)
        ];

        var result = await _organizationService.CreateHub(_model);

        if (result.IsSuccess)
        {
            _navigationManager.NavigateTo("/", true);
        }
        else
        {
            _errorMessage = "Could not create the hub!";
        }

        _isProcessing = false;
        StateHasChanged();
    }

    private Task<bool> ValidateHubCallback(string name, string slug)
    {
        return Task.FromResult(true);
    }

    [Parameter]
    public bool IsVisible { get; set; }

    [Parameter]
    public EventCallback<bool> IsVisibleChanged { get; set; }
}
