namespace Reach.Silo.Migrations;

[AttributeUsage(AttributeTargets.Class)]
internal class MigrationVersionAttribute : Attribute
{
    public MigrationVersionAttribute(int version)
    {
        Version = version;
    }

    public int Version { get; set; }
}
