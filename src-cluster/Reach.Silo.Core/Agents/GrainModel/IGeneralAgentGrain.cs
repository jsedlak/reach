namespace Reach.Silo.Agents.GrainModel;

public interface IGeneralAgentGrain : IGrainWithStringKey
{
    Task<string> SubmitChatMessage(string message);
}
