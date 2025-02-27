using Microsoft.Extensions.Logging;
using Reach.Extensions;
using System.Reflection;

namespace Reach.Silo.Migrations;

internal class MigrationService : IMigrationService
{
    private ILogger<IMigrationService> _logger;

    public MigrationService(ILogger<IMigrationService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<MigrationMetaData> GetMigrations(Assembly assembly, string? baseNamespace = null)
    {
        var allTypes = assembly.GetTypes()
            .Where(m =>
                m.Implements<IMigration>() &&
                !m.IsAbstract
            );

        _logger.LogInformation($"Found {allTypes?.Count() ?? 0} migration types");

        if(allTypes is null || !allTypes.Any())
        {
            return [];
        }

        if (!string.IsNullOrWhiteSpace(baseNamespace))
        {
            allTypes = allTypes
                .Where(m =>
                    m.Namespace is not null &&
                    m.Namespace!.ToLower().StartsWith(baseNamespace.ToLower())
                );

            _logger.LogInformation($"Namespace filtering has narrowed to {allTypes.Count()} types");
        }

        return allTypes
            .Select(m => new MigrationMetaData
            {
                Type = m,
                Version = m.GetCustomAttribute<MigrationVersionAttribute>()?.Version ?? 0
            })
            .OrderBy(m => m.Version);
    }
}
