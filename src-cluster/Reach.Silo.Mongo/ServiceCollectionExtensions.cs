using Microsoft.Extensions.DependencyInjection;
using Reach.Silo.ConfigModel;
using Reach.Silo.Content.ServiceModel;
using Reach.Silo.Content.Services;
using Reach.Silo.Membership.ServiceModel;
using Reach.Silo.Membership.Services;

namespace Reach.Silo;

public static class ServiceCollectionExtensions
{
    public static void AddMongoRepositories(this IServiceCollection services, string databaseName)
    {
        services.Configure<MongoViewRepositoryOptions>(
            options => options.Database = databaseName
        );

        services.AddScoped<IFieldDefinitionViewWriteRepository, MongoFieldDefinitionViewRepository>();
        services.AddScoped<IFieldDefinitionViewReadRepository, MongoFieldDefinitionViewRepository>();
        
        services.AddScoped<IEditorDefinitionViewReadRepository, MongoEditorDefinitionViewRepository>();
        services.AddScoped<IEditorDefinitionViewWriteRepository, MongoEditorDefinitionViewRepository>();
        
        services.AddScoped<IComponentDefinitionViewReadRepository, MongoComponentDefinitionViewRepository>();
        services.AddScoped<IComponentDefinitionViewWriteRepository, MongoComponentDefinitionViewRepository>();
        
        services.AddScoped<IComponentViewReadRepository, MongoComponentViewRepository>();
        services.AddScoped<IComponentViewWriteRepository, MongoComponentViewRepository>();
        
        services.AddScoped<IRendererDefinitionViewReadRepository, MongoRendererDefinitionViewRepository>();
        services.AddScoped<IRendererDefinitionViewWriteRepository, MongoRendererDefinitionViewRepository>();

        services.AddScoped<IAccountViewReadRepository, MongoAccountSettingsViewRepository>();
        services.AddScoped<IAccountViewWriteRepository, MongoAccountSettingsViewRepository>();
    }
}
