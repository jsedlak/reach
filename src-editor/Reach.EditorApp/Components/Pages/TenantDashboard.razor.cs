using Reach.Components;
using Reach.Components.Context;
using Reach.Membership.Views;

namespace Reach.EditorApp.Components.Pages;

public partial class TenantDashboard : BaseTenantComponent
{
    private readonly ITenantContext _tenantContext;
    private AvailableTenantView? _tenant;

    public TenantDashboard(ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _tenant = await _tenantContext.GetCurrentTenant() ?? new();
    }
}
