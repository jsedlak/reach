using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;

namespace Reach.Silo.Tests;

public class EditorDefinitionTests  : DefaultOrleansTestBase
{   
    [Fact]
    public async Task Can_Create_Delete_EditorDefinition()
    {
        var defnId = Guid.NewGuid();
        
        var defn = Grains.GetGrain<IEditorDefinitionGrain>(
            new AggregateId(Guid.AllBitsSet, Guid.AllBitsSet, defnId)
        );

        var response = await defn.Create(new CreateEditorDefinitionCommand(
            Guid.AllBitsSet,
            Guid.AllBitsSet,
            defnId
        )
        {
            Name = "TextBox",
            EditorType = "Reach.Editors.TextBox"
        });

        Assert.True(response.IsSuccess);

        EditorParameterDefinition[] editorDefinitionParameters =
        [
            new EditorParameterDefinition { 
                Name = "MaxLength", 
                DisplayName = "Max Length", 
                Description = "Sets the maximum length of the textbox.", 
                Type = EditorParameterType.Number 
            },
            new EditorParameterDefinition
            {
                Name = "Required",
                DisplayName = "Required",
                Description = "Sets the required value of the textbox.",
                Type = EditorParameterType.Boolean
            }
        ];

        var setParamsResponse = await defn.SetParameters(new SetEditorDefinitionParametersCommand(
            Guid.AllBitsSet,
            Guid.AllBitsSet,
            defnId
        )
        {
            Parameters = editorDefinitionParameters
        });
        
        Assert.True(setParamsResponse.IsSuccess);

        var deleteResponse = await defn.Delete(new DeleteEditorDefinitionCommand(
            Guid.AllBitsSet,
            Guid.AllBitsSet,
            defnId
        ));
        
        Assert.True(deleteResponse.IsSuccess);
    }
}