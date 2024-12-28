namespace Reach.Cqrs;

[GenerateSerializer]
public class CommandResponse
{
    public static CommandResponse Success()
    {
        return new CommandResponse
        {
            IsSuccess = true
        };
    }

    public static CommandResponse Error(IEnumerable<string>? messages = null)
    {
        return new CommandResponse
        {
            IsSuccess = false,
            Messages = (messages ?? Array.Empty<string>()).ToArray()
        };
    }

    [Id(0)]
    public bool IsSuccess { get; set; }

    [Id(1)]
    public string[] Messages { get; set; } = Array.Empty<string>();
}