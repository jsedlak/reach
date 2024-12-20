using Reach.Membership.Views;
using System.ComponentModel;

namespace Reach.Components.Context;

/// <summary>
/// Contains data related to the currently viewed and available tenants
/// </summary>
public sealed class TenantContext : INotifyPropertyChanged
{
    private AvailableHubView? _currentHub;
    private IEnumerable<AvailableHubView> _availableHubs = [];
    private IEnumerable<AvailableOrganizationView> _availableOrganizations = [];

    /// <summary>
    /// Gets or Sets the current tenant being viewed
    /// </summary>
    public AvailableHubView? CurrentHub
    {
        get { return _currentHub; }
        set
        {
            _currentHub = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentHub)));
        }
    }

    /// <summary>
    /// Gets or Sets the list of tenants the user has access to
    /// </summary>
    public IEnumerable<AvailableOrganizationView> AvailableOrganizations
    {
        get { return _availableOrganizations; }
        set
        {
            _availableOrganizations = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableOrganizations)));
        }
    }

    /// <summary>
    /// Gets or Sets the list of tenants the user has access to
    /// </summary>
    public IEnumerable<AvailableHubView> AvailableHubs
    {
        get { return _availableHubs; }
        set
        {
            _availableHubs = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableHubs)));
        }
    }

    /// <summary>
    /// Fires when a property has changed
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}
