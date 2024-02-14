using Petl;

namespace ReachCms.Cqrs;

public sealed class CommandResult : IRequestResult
{
    public static CommandResult Success(params IAggregateEvent[] responses)
    {
        return new CommandResult
        {
            IsSuccess = true,
            Responses = responses
        };
    }

    public static CommandResult Error()
    {
        return new CommandResult
        {
            IsSuccess = false
        };
    }

    public bool IsSuccess { get; set; }

    public IEnumerable<IResponse> Responses { get; set; } = Enumerable.Empty<IResponse>();
}
