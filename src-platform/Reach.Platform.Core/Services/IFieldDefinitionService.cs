using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Platform.Services
{
    public interface IFieldDefinitionService
    {
        Task<CommandResponse> Create(CreateFieldDefinitionCommand command);
        Task<IEnumerable<FieldDefinitionView>> GetFieldDefinitons(Guid organizationId, Guid hubId, string? query = null);
    }
}