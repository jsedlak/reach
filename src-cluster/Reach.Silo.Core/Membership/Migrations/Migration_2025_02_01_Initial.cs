using Reach.Content.Model;
using Reach.Silo.Migrations;

namespace Reach.Silo.Membership.Migrations;

[MigrationVersion(0)]
public class Migration_2025_02_01_Initial : MembershipMigrationBase
{
    public Migration_2025_02_01_Initial(IGrainFactory grainFactory)
        : base(grainFactory)
    {
    }

    public override async Task Forwards(Guid organizationId, Guid hubId)
    {
        var editorDefnId = Guid.NewGuid();
        await CreateEditorDefinition(
            organizationId,
            hubId,
            editorDefnId,
            "TextEditor",
            "textEditor",
            "Reach.Editors.Text.TextEditor",
            [
                new EditorParameterDefinition{ DisplayName = "Max Length", Name = "MaxLength", Description = "", Type = EditorParameterType.Number }
            ]
        );

        var fieldDefnId = Guid.NewGuid();
        await CreateFieldDefinition(
            organizationId,
            hubId,
            fieldDefnId,
            "Single Line Text",
            "single-line-text",
            editorDefnId
        );
    }
}
