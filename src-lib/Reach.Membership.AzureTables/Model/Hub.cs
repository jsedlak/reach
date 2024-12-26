using Azure;
using Azure.Data.Tables;

namespace Reach.Membership.Model;

internal sealed class Hub : ITableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string OwnerIdentifier { get; set; } = null!;

    public string RegionKey { get; set; } = string.Empty;

    public string IconUrl { get; set; } = string.Empty;

    public Guid OrganizationId { get; set; } = Guid.Empty;

    #region Azure Table Stuff
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; } = ETag.All;
    #endregion
}