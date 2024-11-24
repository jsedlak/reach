using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Reach.Membership.Views;
using System.ComponentModel;

namespace Reach.Components.Context;

public sealed class TenantContext : ITenantContext
{
    private static int GlobalId = 1;
    private readonly int _id = GlobalId++;
    private readonly NavigationManager _navigationManager;
    private readonly Func<Task<IEnumerable<AvailableTenantView>>> _getTenants;

    private IEnumerable<AvailableTenantView> _tenants = [];

    public event PropertyChangedEventHandler? PropertyChanged;

    public TenantContext(Func<Task<IEnumerable<AvailableTenantView>>> getTenants, NavigationManager navigationManager)
    {
        _getTenants = getTenants;
        _navigationManager = navigationManager;
    }

    public async Task<IEnumerable<AvailableTenantView>> GetAllTenants()
    {
        _tenants = await _getTenants();
        return _tenants;
    }

    public async Task<AvailableTenantView?> GetCurrentTenant()
    {
        if(!_tenants.Any())
        {
            await GetAllTenants();
        }

        var path = _navigationManager.ToBaseRelativePath(_navigationManager.Uri).ToLower();

        var pathSplit = path.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        if (pathSplit.Length > 1 && pathSplit[0].Equals("app"))
        {
            return _tenants.FirstOrDefault(m => m.Slug == pathSplit[1]);
        }

        return null;
    }

    public int Id { get { return _id; } }
}
