using Reach.Content.ServiceModel;
using Reach.Silo.Content.ServiceModel;
using Reach.Silo.Content.Services;

namespace Reach.Silo.Host.Extensions;

public static class RepositoryExtensions
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoViewRepositoryOptions>(options => options.Database = "reach");

        builder.Services.AddScoped<IFieldDefinitionViewWriteRepository, MongoFieldDefinitionViewRepository>();
        builder.Services.AddScoped<IFieldDefinitionViewReadRepository, MongoFieldDefinitionViewRepository>();

        builder.Services.AddScoped<IEditorDefinitionViewReadRepository, MongoEditorDefinitionViewRepository>();
        builder.Services.AddScoped<IEditorDefinitionViewWriteRepository, MongoEditorDefinitionViewRepository>();
        
        builder.Services.AddScoped<IComponentDefinitionViewReadRepository, MongoComponentDefinitionViewRepository>();
        builder.Services.AddScoped<IComponentDefinitionViewWriteRepository, MongoComponentDefinitionViewRepository>();

        builder.Services.AddScoped<IComponentViewReadRepository, MongoComponentViewRepository>();
        builder.Services.AddScoped<IComponentViewWriteRepository, MongoComponentViewRepository>();
        
        builder.Services.AddScoped<IRendererDefinitionViewReadRepository, MongoRendererDefinitionViewRepository>();
        builder.Services.AddScoped<IRendererDefinitionViewWriteRepository, MongoRendererDefinitionViewRepository>();
        
        return builder;
    }
}
