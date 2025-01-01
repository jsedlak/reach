using Newtonsoft.Json;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Commands.Components;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;

namespace Reach.Silo.Tests;

public class ComponentTests : DefaultOrleansTestBase
{
    private readonly Guid OrganizationId = Guid.NewGuid();
    private readonly Guid HubId = Guid.NewGuid();

    private readonly Guid EditorDefinition_TextBox_Id = Guid.NewGuid();
    private readonly Guid EditorDefinition_Asset_Id = Guid.NewGuid();
    private readonly Guid EditorDefinition_Link_Id = Guid.NewGuid();

    private readonly Guid FieldDefinition_SingleLineText_Id = Guid.NewGuid();
    private readonly Guid FieldDefinition_SingleAsset_Id = Guid.NewGuid();
    private readonly Guid FieldDefinition_Link_Id = Guid.NewGuid();

    private readonly Guid ComponentDefinition_Hero_Id = Guid.NewGuid();
    
    private readonly Guid Component_HomepageHero_Id = Guid.NewGuid();

    private async Task BuildEditorDefinitions()
    {
        #region TextBox

        var defn = Grains.GetGrain<IEditorDefinitionGrain>(
            new AggregateId(OrganizationId, HubId, EditorDefinition_TextBox_Id)
        );

        var response = await defn.Create(
            new CreateEditorDefinitionCommand(OrganizationId, HubId, EditorDefinition_TextBox_Id)
            {
                Name = "TextBox",
                EditorType = "Reach.Components.Editors.TextBox"
            });

        Assert.True(response.IsSuccess);

        EditorParameterDefinition[] editorDefinitionParameters =
        [
            new EditorParameterDefinition
            {
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
            EditorDefinition_TextBox_Id
        )
        {
            Parameters = editorDefinitionParameters
        });

        Assert.True(setParamsResponse.IsSuccess);

        #endregion

        #region Asset

        var assetDefn =
            Grains.GetGrain<IEditorDefinitionGrain>(new AggregateId(OrganizationId, HubId, EditorDefinition_Asset_Id));

        await assetDefn.Create(new CreateEditorDefinitionCommand(OrganizationId, HubId, EditorDefinition_Asset_Id)
        {
            Name = "Asset",
            EditorType = "Reach.Components.Editors.AssetReference"
        });

        #endregion

        #region Link

        var linkDefn =
            Grains.GetGrain<IEditorDefinitionGrain>(new AggregateId(OrganizationId, HubId, EditorDefinition_Link_Id));

        await linkDefn.Create(new CreateEditorDefinitionCommand(OrganizationId, HubId, EditorDefinition_Link_Id)
        {
            Name = "Link",
            EditorType = "Reach.Components.Editors.Link"
        });

        #endregion
    }

    private async Task BuildFieldDefinitions()
    {
        #region SingleLineText

        var singleLineTextDefn =
            Grains.GetGrain<IFieldDefinitionGrain>(new AggregateId(OrganizationId, HubId,
                FieldDefinition_SingleLineText_Id));

        await singleLineTextDefn.Create(new CreateFieldDefinitionCommand
        {
            Name = "Single Line Text",
            Slug = "singleLineText",
            EditorDefinitionId = EditorDefinition_TextBox_Id
        });

        #endregion

        #region SingleAsset

        var singleAssetDefn =
            Grains.GetGrain<IFieldDefinitionGrain>(new AggregateId(OrganizationId, HubId,
                FieldDefinition_SingleAsset_Id));

        await singleAssetDefn.Create(new CreateFieldDefinitionCommand
        {
            Name = "Single Asset",
            Slug = "singleAsset",
            EditorDefinitionId = EditorDefinition_Asset_Id
        });

        #endregion

        #region Link

        var linkDefn =
            Grains.GetGrain<IFieldDefinitionGrain>(new AggregateId(OrganizationId, HubId, FieldDefinition_Link_Id));

        await linkDefn.Create(new CreateFieldDefinitionCommand
        {
            Name = "Link",
            Slug = "link",
            EditorDefinitionId = EditorDefinition_Link_Id
        });

        #endregion
    }

    private async Task BuildComponentDefinitions()
    {
        var heroDefn =
            Grains.GetGrain<IComponentDefinitionGrain>(new AggregateId(OrganizationId, HubId,
                ComponentDefinition_Hero_Id));

        var response = await heroDefn.Create(new CreateComponentDefinitionCommand
        {
            Name = "Hero",
            Slug = "hero"
        });

        Assert.True(response.IsSuccess);

        await heroDefn.AddField(
            new AddFieldToComponentDefinitionCommand(OrganizationId, HubId, ComponentDefinition_Hero_Id)
            {
                Name = "Title",
                Slug = "title",
                FieldDefinitionId = FieldDefinition_SingleLineText_Id
            });

        await heroDefn.AddField(
            new AddFieldToComponentDefinitionCommand(OrganizationId, HubId, ComponentDefinition_Hero_Id)
            {
                Name = "Background Image",
                Slug = "backgroundImage",
                FieldDefinitionId = FieldDefinition_SingleAsset_Id
            });

        await heroDefn.AddField(
            new AddFieldToComponentDefinitionCommand(OrganizationId, HubId, ComponentDefinition_Hero_Id)
            {
                Name = "CTA Link",
                Slug = "ctaLink",
                FieldDefinitionId = FieldDefinition_Link_Id
            });
    }

    [Fact]
    public async Task Can_Create_Components()
    {
        await BuildEditorDefinitions();
        await BuildFieldDefinitions();
        await BuildComponentDefinitions();

        var component =
            Grains.GetGrain<IComponentGrain>(new AggregateId(OrganizationId, HubId, Component_HomepageHero_Id));

        var createResponse = await component.Create(
            new CreateComponentCommand(OrganizationId, HubId, Component_HomepageHero_Id)
            {
                Name = "Homepage Hero",
                Slug = "homepage-hero"
            });
        
        Assert.True(createResponse.IsSuccess);

        var setFieldValueResponse = await component.SetFieldValue(
            new SetComponentFieldValueCommand(OrganizationId, HubId, Component_HomepageHero_Id)
            {
                FieldKey = "title",
                Value = "This is the title field!"
            });
        
        Assert.True(setFieldValueResponse.IsSuccess);

        var imgResponse = await component.SetFieldValue(
            new SetComponentFieldValueCommand(OrganizationId, HubId, Component_HomepageHero_Id)
            {
                FieldKey = "backgroundImage",
                Value = Guid.NewGuid().ToString()
            });
        
        Assert.True(imgResponse.IsSuccess);

        var ctaResponse = await component.SetFieldValue(
            new SetComponentFieldValueCommand(OrganizationId, HubId, Component_HomepageHero_Id)
            {
                FieldKey = "ctaLink",
                Value = JsonConvert.SerializeObject(new
                    { url = "https://google.com", text = "Google", target = "_blank" })
            });
        
        Assert.True(ctaResponse.IsSuccess);
    }
}