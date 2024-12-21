using Microsoft.Extensions.Hosting;
using Reach.Membership.Postgres.Data;

namespace Reach.Membership.Postgres;

public static class RegistrationExtensions
{
    public static IHostApplicationBuilder RegisterPostgresMembership(this IHostApplicationBuilder builder, string connectionName =  "membership-database")
    {
        builder.AddNpgsqlDbContext<MembershipDbContext>(connectionName);
        return builder;
    }
}
