using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Reach.Silo.Agents.Plugins;

namespace Reach.Silo.Agents;

public static class ServiceCollectionExtensions
{
    private const string OllamaProviderName = "Ollama";
    private const string OpenAIProviderName = "OpenAI";

    private static void RegisterOllama(IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("[AGENTS] Registering Ollama as AI Chat Provider");

        var ollamaConfig = new OllamaConfig();
        configuration.GetSection("Ollama").Bind(ollamaConfig);

        #pragma warning disable SKEXP0070
        services.AddOllamaChatCompletion(
            modelId: ollamaConfig.Model,
            endpoint: new Uri(ollamaConfig.Endpoint)
        );
    }
    private static void RegisterOpenAi(IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("[AGENTS] Registering OpenAI as AI Chat Provider");

        var openAiConfig = new OpenAiConfig();
        configuration.GetSection("OpenAi").Bind(openAiConfig);

        services.AddOpenAIChatCompletion(
            modelId: openAiConfig.Model,
            apiKey: openAiConfig.ApiKey
        );
    }

    public static IServiceCollection AddOllamaKernel(this IServiceCollection services, IConfiguration configuration)
    {
        var aiChat = configuration.GetValue<string>("AI_CHAT") ?? OpenAIProviderName;

        if (aiChat.Equals(OllamaProviderName, StringComparison.OrdinalIgnoreCase))
        {
            RegisterOllama(services, configuration);
        }
        else
        {
            RegisterOpenAi(services, configuration);
        }

        services.AddScoped((serviceProvider) =>
        {
            var kernel = new Kernel(serviceProvider);

            kernel.Plugins.AddFromType<EditorDefinitionPlugin>(
                "Editor_Definitions",
                serviceProvider
            );

            kernel.Plugins.AddFromType<FieldDefinitionPlugin>(
                "Field_Definitions", 
                serviceProvider
            );

            kernel.Plugins.AddFromType<ComponentDefinitionPlugin>(
                "Component_Definitions",
                serviceProvider
            );

            kernel.Plugins.AddFromType<ComponentPlugin>(
                "Components",
                serviceProvider
            );

            return kernel;
        });

        return services;
    }

    private class OpenAiConfig
    {
        public string ApiKey { get; set; } = "";

        public string Model { get; set; } = "";
    }

    private class OllamaConfig
    {
        public string Endpoint { get; set; } = "";

        public string Model { get; set; } = "";
    }
}
