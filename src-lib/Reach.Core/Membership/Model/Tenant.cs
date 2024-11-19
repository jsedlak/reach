using Azure;
using Azure.Data.Tables;

namespace Reach.Membership.Model;

public class Tenant : ITableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string OwnerIdentifier { get; set; } = null!;

    public string RegionId { get; set; } = string.Empty;

    #region Azure Table Stuff
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; } = ETag.All;
    #endregion
}
