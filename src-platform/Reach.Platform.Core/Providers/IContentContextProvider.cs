using Reach.Content.Views;

namespace Reach.Platform.Providers;

public interface IContentContextProvider : IContextProvider
{
    IEnumerable<EditorDefinitionView> EditorDefinitions { get; }
}