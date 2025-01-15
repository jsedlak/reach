using Reach.Content.Views;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

internal class HttpEditorService : IEditorService
{
    private readonly IGraphClient _graphClient;

    public HttpEditorService(IGraphClient graphClient)
    {
        _graphClient = graphClient;
    }

    public async Task<IEnumerable<EditorView>> GetEditors()
    {
        return await _graphClient.GetMany<EditorView>();
    }
}
