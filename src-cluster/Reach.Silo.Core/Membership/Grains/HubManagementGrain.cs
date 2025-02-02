using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Membership.GrainModel;
using Reach.Silo.Membership.Migrations;
using Reach.Silo.Migrations;

namespace Reach.Silo.Membership.Grains;

internal class HubManagementGrain : Grain, IHubManagementGrain
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMigrationService _migrationService;
    private readonly ILogger<IHubManagementGrain> _logger;

    private readonly IPersistentState<MigrationState> _migrationState;

    public HubManagementGrain(
        [PersistentState("migrationState")] IPersistentState<MigrationState> migrationState,
        IMigrationService migrationService,
        IServiceProvider serviceProvider, 
        ILogger<IHubManagementGrain> logger)
    {
        _migrationState = migrationState;
        _migrationService = migrationService;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    private (Guid organizationId, Guid hubId) GetIds()
    {
        var keys = this.GetPrimaryKeyString()
            .Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        return (
            Guid.Parse(keys[0]),
            Guid.Parse(keys[1])
        );  
    }

    public async Task<CommandResponse> Initialize()
    {
        var identifier = GetIds();

        var migrationBaseType = typeof(Migration_2025_02_01_Initial);

        var migrations = _migrationService.GetMigrations(
            migrationBaseType.Assembly,
            migrationBaseType.Namespace
        );

        foreach(var migration in migrations)
        {
            var migrationInstance = ActivatorUtilities.CreateInstance(
                _serviceProvider,
                migration.Type
            );

            if(migrationInstance is null)
            {
                _logger.LogError($"Could not create an instance of the {migration.Type.Name} migration class");
                continue;
            }

            if(migrationInstance is IMigration convertedInstance)
            {
                await convertedInstance.Forwards(
                    identifier.organizationId,
                    identifier.hubId
                );

                _migrationState.State.Version = migration.Version;
                await _migrationState.WriteStateAsync();
            }
        }

        return CommandResponse.Success();
    }

    public Task<CommandResponse> Upgrade()
    {
        return Task.FromResult(
            CommandResponse.Success()
        );
    }
}