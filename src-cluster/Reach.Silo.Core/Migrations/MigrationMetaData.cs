namespace Reach.Silo.Migrations;

internal sealed class MigrationMetaData
{
    public required Type Type { get; init; }

    public required int Version { get; init; }
}
