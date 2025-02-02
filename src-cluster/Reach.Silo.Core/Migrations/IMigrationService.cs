using System.Reflection;

namespace Reach.Silo.Migrations
{
    internal interface IMigrationService
    {
        IEnumerable<MigrationMetaData> GetMigrations(Assembly assembly, string? baseNamespace = null);
    }
}