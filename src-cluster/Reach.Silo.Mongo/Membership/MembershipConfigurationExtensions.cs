using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Reach.Membership.Views;

namespace Reach.Silo.Membership;

internal static class MembershipConfigurationExtensions
{
    public static void ConfigureMembership(this IServiceCollection services)
    {
        BsonClassMap.RegisterClassMap<AccountSettingsViewClassMap>();
    }

    public class AccountSettingsViewClassMap : BsonClassMap<AccountSettingsView>
    {
        public AccountSettingsViewClassMap()
        {
            AutoMap(); 
            MapIdMember(c => c.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId)); 
        }
    }
}
