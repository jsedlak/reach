using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Components.Context;
using Reach.EditorApp.Client.Authentication;

namespace Reach.EditorApp.Client.Components.Layout.Tenants;

public partial class Toolbar : ComponentBase, IDisposable
{
    private UserContext _userContext = new UserContext("","","");
    ~Toolbar()
    {
        Dispose(false);
    }

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationState is not null)
        {
            _userContext = (await AuthenticationState).AsContext();
        }

        TenantContext.PropertyChanged += OnTenantContextChanged;
    }

    private void OnTenantContextChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        StateHasChanged();
    }

    #region Disposing
    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            GC.SuppressFinalize(this);
        }

        TenantContext.PropertyChanged -= OnTenantContextChanged;
    }
    #endregion

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

    [CascadingParameter]
    private TenantContext TenantContext { get; set; } = null!;
}
