using Reach.Cqrs;

namespace Reach.Platform.ServiceModel;

public interface ICommandClient
{
    Task<CommandResponse> Execute<TCommand>(string endpoint, TCommand command);
}