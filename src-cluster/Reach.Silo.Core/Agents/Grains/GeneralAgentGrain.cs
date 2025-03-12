using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Reach.Cqrs;
using Reach.Silo.Agents.GrainModel;

namespace Reach.Silo.Agents.Grains;

public class GeneralAgentGrain : Grain, IGeneralAgentGrain
{
    private readonly Kernel _kernel = null!;
    private readonly IChatCompletionService _chatCompletionService = null!;
    private readonly PromptExecutionSettings _executionSettings = new()
    {
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
    };
    private ILogger<IGeneralAgentGrain> _logger;

    public GeneralAgentGrain(Kernel kernel, IChatCompletionService chatCompletionService, ILogger<IGeneralAgentGrain> logger)
    {
        _kernel = kernel;
        _chatCompletionService = chatCompletionService;
        _logger = logger;
    }

    public async Task<string> SubmitChatMessage(string message)
    {
        _logger.LogInformation("[AGENTS] Chat Submission on [{PrimaryKey}]", this.GetPrimaryKeyString());

        var aggId = ResourceId.Parse(this.GetPrimaryKeyString());

        var chatHistory = new ChatHistory();

        //_kernel.Data.Add("OrganizationId", aggId.OrganizationId);
        //_kernel.Data.Add("HubId", aggId.HubId);

        chatHistory.AddDeveloperMessage($"The organizationId is {aggId.OrganizationId} and the hubId is {aggId.HubId}.");
        chatHistory.AddUserMessage(message);

        _logger.LogInformation("[AGENTS] Submitting chat message");

        var response = await _chatCompletionService.GetChatMessageContentsAsync(
            chatHistory,
            kernel: _kernel,
            executionSettings: _executionSettings
        );

        var agentResponseContent = response[^1]?.Content ?? "Error - could not retrieve a response.";

        _logger.LogInformation("[AGENTS] Agent returned {Message}", agentResponseContent);

        return agentResponseContent;
    }
}
