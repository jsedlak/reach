using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reach.Membership.Postgres.Data;
using Reach.Membership.Postgres.Services;
using Reach.Membership.ServiceModel;

namespace Reach.Membership.Postgres;

public static class RegistrationExtensions
{
    /// <summary>
    /// Registers Postgres based services for handling the membership set of services
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="connectionName"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder AddPostgresMembership(
        this IHostApplicationBuilder builder, 
        string connectionName =  "membership-database")
    {
        builder.AddNpgsqlDbContext<MembershipDbContext>(connectionName);
        builder.Services.AddScoped<IOrganizationService, PostgresOrganizationService>();

        return builder;
    }

    public static IHost UsePostgresMembership(this IHost host)
    {
        host.Services.GetRequiredService<MembershipDbContext>().Database.EnsureCreated();
        return host;
    }
}
