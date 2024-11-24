using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace CascadingContextProvider;

/// <summary>
/// Represents a concept of storing user specific contextual data in a POCO
/// </summary>
public sealed class TenantContext : INotifyPropertyChanged
{
    private string _name = "unset";

    private NavigationManager _navigation;

    public TenantContext(NavigationManager navigation)
    {
        _navigation = navigation;
        _navigation.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
        Name = new Uri(_navigation.Uri).PathAndQuery.Split(["/"], StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "NULL";
    }

    /// <summary>
    /// Gets or Sets the name of the tenant currently being used
    /// </summary>
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }

    /// <summary>
    /// Fires when a property's value has changed
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}
