using Microsoft.AspNetCore.Components;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using Tazor.Components.Theming;

namespace Reach.Platform.Components.Membership;

public partial class OrganizationSelector : BaseReachComponent
{
    private readonly IThemeManager _themeManager;
    private readonly IOrganizationService _organizationService;
    private IEnumerable<AvailableOrganizationView> _organizations = [];

    public OrganizationSelector(IThemeManager themeManager, IOrganizationService organizationService)
    {
        _themeManager = themeManager;
        _organizationService = organizationService;
    }

    private async Task OnOrganizationIdChanged(ChangeEventArgs args)
    {
        if (Guid.TryParse(args.Value?.ToString() ?? Guid.Empty.ToString(), out var newValue))
        {
            SelectedOrganizationId = newValue;
            await SelectedOrganizationIdChanged.InvokeAsync(SelectedOrganizationId);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine(nameof(OnInitializedAsync) + $" IsReady={IsReady}");

        if (IsReady)
        {
            _organizations = await _organizationService.GetAvailableOrganizations();
        }
    }

    protected override async Task OnComponentIsReady()
    {
        Console.WriteLine(nameof(OnComponentIsReady));

        _organizations = await _organizationService.GetAvailableOrganizations();
    }

    [Parameter]
    public Guid SelectedOrganizationId { get; set; }

    [Parameter]
    public EventCallback<Guid> SelectedOrganizationIdChanged { get; set; }
}
