using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reach.Cqrs;
using Reach.Silo.Agents.GrainModel;

namespace Reach.Silo.Host.Agents;

public static class WebApplicationExtensions
{
    public static WebApplication MapAgentEndpoints(this WebApplication application)
    {
        application.MapPost(
            $"/api/agents/{{organizationId}}/{{hubId}}/general",
            async (
                [FromRoute] Guid organizationId,
                [FromRoute] Guid hubId,
                [FromServices] IClusterClient clusterClient,
                HttpRequest httpRequest
            ) =>
            {
                var grain = clusterClient.GetGrain<IGeneralAgentGrain>(
                    new AggregateId(organizationId, hubId, Guid.Empty)
                );

                string body = "";
                using (StreamReader stream = new StreamReader(httpRequest.Body))
                {
                    body = await stream.ReadToEndAsync();
                }

                var request = JsonConvert.DeserializeObject<ChatRequest>(body);
                
                if(request == null)
                {
                    return new ChatResponse
                    {
                        Message = "Invalid input."
                    };
                }

                var response = await grain.SubmitChatMessage(request.Message);
                return new ChatResponse { Message = response };
            }
        );

        return application;
    }

    private class ChatResponse
    {
        public string Message { get; set; } = null!;
    }

    private class ChatRequest
    {
        public string Message { get; set; } = null!;
    }
}
