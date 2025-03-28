﻿using Reach.Content.Model;
using Reach.Silo.Migrations;

namespace Reach.Silo.Membership.Migrations;

[MigrationVersion(0)]
public class Migration_2025_02_01_Initial : MembershipMigrationBase
{
    public Migration_2025_02_01_Initial(IGrainFactory grainFactory)
        : base(grainFactory)
    {
    }

    private IEnumerable<EditorParameterDefinition> CreateDefaultParameters()
    {
        return [
            new EditorParameterDefinition { DisplayName = "Required", Name = "Required", Description = "Whether the field must have a value before saving the content.", Type = EditorParameterType.Boolean }
        ];
    }

    public override async Task Forwards(Guid organizationId, Guid hubId)
    {
        #region Editors - Text
        var textEditorDefnId = Guid.NewGuid();
        await CreateEditorDefinition(
            organizationId,
            hubId,
            textEditorDefnId,
            "TextEditor",
            "textEditor",
            "Reach.Editors.Text.TextEditor, Reach.Editors",
            [
                ..CreateDefaultParameters(),
                new EditorParameterDefinition { DisplayName = "Multi-Line", Name = "Multiline", Description = "", Type = EditorParameterType.Boolean },
                new EditorParameterDefinition { DisplayName = "Max Length", Name = "MaxLength", Description = "", Type = EditorParameterType.Number },
            ]
        );

        var numberEditorDefnId = Guid.NewGuid();
        await CreateEditorDefinition(
            organizationId,
            hubId,
            numberEditorDefnId,
            "NumberEditor",
            "numberEditor",
            "Reach.Editors.Text.NumberEditor, Reach.Editors",
            [
                ..CreateDefaultParameters(),
                new EditorParameterDefinition { DisplayName = "Decimals", Name = "Decimals", Description = "The number of decimals to allow", Type = EditorParameterType.Number },
                new EditorParameterDefinition { DisplayName = "Format", Name = "Format", Description = "The format to display the value of", Type = EditorParameterType.Number },
                new EditorParameterDefinition { DisplayName = "Unit", Name = "Unit", Description = "The unit of the number", Type = EditorParameterType.Number },
            ]
        );
        #endregion

        #region Editors - Unit
        var singleUnitBoxDefnId = Guid.NewGuid();
        await CreateEditorDefinition(
            organizationId,
            hubId,
            singleUnitBoxDefnId,
            "SingleUnitBox",
            "singleUnitBox",
            "Reach.Editors.Measurement.SingleUnitBox, Reach.Editors",
            [
                ..CreateDefaultParameters(),
                new EditorParameterDefinition { DisplayName = "Units", Name = "Units", Description = "Comma separated list of units to support", Type = EditorParameterType.Text }
            ]
        );
        #endregion


        #region Editors - General
        var booleanEditorDefnId = Guid.NewGuid();
        await CreateEditorDefinition(
            organizationId,
            hubId,
            booleanEditorDefnId,
            "BooleanEditor",
            "booleanEditor",
            "Reach.Editors.General.BooleanEditor, Reach.Editors",
            [
                ..CreateDefaultParameters()
            ]
        );
        #endregion

        #region Fields - Text
        var textFieldDefnId = Guid.NewGuid();
        await CreateFieldDefinition(
            organizationId,
            hubId,
            textFieldDefnId,
            "Single Line Text",
            "single-line-text",
            textEditorDefnId
        );

        var multiLineTextFieldDefnId = Guid.NewGuid();
        await CreateFieldDefinition(
            organizationId,
            hubId,
            multiLineTextFieldDefnId,
            "Multi-Line Text",
            "multi-line-text",
            textEditorDefnId,
            [
                new EditorParameter { Key = "Multiline", Value = false.ToString() }
            ]
        );

        var numberFieldDefnId = Guid.NewGuid();
        await CreateFieldDefinition(
            organizationId,
            hubId,
            numberFieldDefnId,
            "Number",
            "number",
            numberEditorDefnId
        );
        #endregion

        #region Fields - Units
        var singleUnitBoxFieldDefnId = Guid.NewGuid();
        await CreateFieldDefinition(
            organizationId,
            hubId,
            singleUnitBoxFieldDefnId,
            "Single Unit Box",
            "single-unit-box",
            singleUnitBoxDefnId,
            [
                new EditorParameter { Key = "Units", Value = "px,%,em,rem,vw,vh" }
            ]
        );
        #endregion

        #region Hero
        var heroDefnId = Guid.NewGuid();
        await CreateComponentDefinition(
            organizationId,
            hubId,
            heroDefnId,
            "Hero Carousel",
            "hero-carousel"
        );

        await AddFieldToComponentDefinition(
            organizationId,
            hubId,
            heroDefnId,
            "Title",
            "title",
            textFieldDefnId
        );

        await AddFieldToComponentDefinition(
            organizationId,
            hubId,
            heroDefnId,
            "Text",
            "text",
            textFieldDefnId
        );

        var heroId = Guid.NewGuid();
        await CreateComponent(
            organizationId,
            hubId,
            heroId,
            "Sample Hero Carousel",
            "sample-hero-carousel",
            heroDefnId
        );
        #endregion
    }
}
