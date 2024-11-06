namespace Reach.Cqrs;

[GenerateSerializer]
public class CommandResponse
{
    [Id(0)]
    public bool IsSuccess { get; set; }
}