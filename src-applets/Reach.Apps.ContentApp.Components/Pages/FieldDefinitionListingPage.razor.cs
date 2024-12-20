using Reach.Apps.ContentApp.Services;
using Reach.Content.Commands.Fields;
using Reach.Content.Views;
using Reach.Security;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class FieldDefinitionListingPage : ContentBasePage
{
    private IEnumerable<FieldDefinitionView> _fieldDefinitions = [];

    private DialogContext<CreateFieldDefinitionCommand> _createContext = new(() => { });

    public FieldDefinitionListingPage(FieldDefinitionService fieldDefinitionService)
    {
        FieldDefinitionService = fieldDefinitionService;

        _createContext = new(StateHasChanged);
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
        _fieldDefinitions = await FieldDefinitionService.GetFieldDefinitons();
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
            if (model == null)
            {
                return (false, ["Invalid data."]);
            }

            var result = await FieldDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }

    private FieldDefinitionService FieldDefinitionService { get; }
}
