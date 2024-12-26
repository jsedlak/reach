using Azure;
using Azure.Data.Tables;

namespace Reach.Membership.Model;

internal sealed class Organization : ITableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string OwnerIdentifier { get; set; } = null!;

    #region Azure Table Stuff
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; } = ETag.All;
    #endregion
}
