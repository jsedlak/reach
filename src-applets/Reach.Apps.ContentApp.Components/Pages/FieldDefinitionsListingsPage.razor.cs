using Reach.Apps.ContentApp.Services;
using Reach.Content.Commands.Editors;
using Reach.Content.Commands.Fields;
using Reach.Content.Views;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

public partial class FieldDefinitionsListingsPage : ContentBasePage
{
    private readonly FieldDefinitionService _fieldDefinitionService;
    private IEnumerable<FieldDefinitionView> _fieldDefinitions = [];

    private DialogContext<CreateFieldDefinitionCommand> _createContext = new(() => { });

    public FieldDefinitionsListingsPage(FieldDefinitionService fieldDefinitionService)
    {
        _createContext = new(StateHasChanged);
        _fieldDefinitionService = fieldDefinitionService;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await RefreshData();
            StateHasChanged();
        }
    }

    private async Task RefreshData()
    {
        _fieldDefinitions = await _fieldDefinitionService.GetFieldDefinitons();
    }

    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new CreateFieldDefinitionCommand(Guid.NewGuid()));
    }

    private Task OnCancelCreateClicked()
    {
        return _createContext.Cancel();
    }

    private async Task OnConfirmCreateClicked()
    {
        await _createContext.Confirm(async (model) =>
        {
            var result = await _fieldDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }
}
