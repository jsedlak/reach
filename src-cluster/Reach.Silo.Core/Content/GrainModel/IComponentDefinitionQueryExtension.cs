using Reach.Content.Model;

namespace Reach.Silo.Content.GrainModel;

public interface IComponentDefinitionQueryExtension : IGrainExtension
{
    Task<Field[]> GetFields();
}