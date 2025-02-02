namespace Reach.Silo.Migrations;

internal interface IMigration
{
    Task Forwards(Guid organizationId, Guid hubId);
}