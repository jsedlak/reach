namespace Reach.Orchestration.Model;

public class Region
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Key { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ApiUrl { get; set; } = null!;

    public string GraphUrl { get; set; } = null!;
}
