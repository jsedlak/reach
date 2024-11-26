using Reach.Membership.Views;
using System.ComponentModel;

namespace Reach.Components.Context;

/// <summary>
/// Contains data related to the currently viewed and available tenants
/// </summary>
public sealed class TenantContext : INotifyPropertyChanged
{
    private AvailableTenantView? _currentTenant;
    private IEnumerable<AvailableTenantView> _availableTenantViews = [];

    /// <summary>
    /// Gets or Sets the current tenant being viewed
    /// </summary>
    public AvailableTenantView? CurrentTenant
    {
        get { return _currentTenant; }
        set
        {
            _currentTenant = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTenant)));
        }
    }

    /// <summary>
    /// Gets or Sets the list of tenants the user has access to
    /// </summary>
    public IEnumerable<AvailableTenantView> AvailableTenants
    {
        get { return _availableTenantViews; }
        set
        {
            _availableTenantViews = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableTenants)));
        }
    }

    /// <summary>
    /// Fires when a property has changed
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}
