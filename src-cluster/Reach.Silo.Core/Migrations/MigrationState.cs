namespace Reach.Silo.Migrations;

[GenerateSerializer]
public class MigrationState
{
    [Id(0)]
    public int Version { get; set; }
}
