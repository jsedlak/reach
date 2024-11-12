using Reach.Content.ServiceModel;
using Reach.Silo.Content.ServiceModel;
using Reach.Silo.Content.Services;

namespace Reach.Silo.Host.Extensions;

public static class RepositoryExtensions
{
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoViewRepositoryOptions>(options => options.Database = "reach");

        builder.Services.AddSingleton<IFieldDefinitionViewWriteRepository, MongoFieldDefinitionViewRepository>();
        builder.Services.AddSingleton<IFieldDefinitionViewReadRepository, MongoFieldDefinitionViewRepository>();

        builder.Services.AddSingleton<IEditorDefinitionViewReadRepository, MongoEditorDefinitionViewRepository>();
        return builder;
    }
}
