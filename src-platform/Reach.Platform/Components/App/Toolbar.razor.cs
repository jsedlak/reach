using Microsoft.AspNetCore.Components;

namespace Reach.Platform.Components.App;

public partial class Toolbar : ComponentBase, IDisposable
{
    
    ~Toolbar()
    {
        Dispose(false);
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
    }
    #endregion
}
