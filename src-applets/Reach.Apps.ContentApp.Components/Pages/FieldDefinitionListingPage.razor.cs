using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Views;
using Reach.Platform.Providers;
using Reach.Platform.ServiceModel;
using Reach.Security;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class FieldDefinitionListingPage : ContentBasePage
{
    private readonly IContentContextProvider _contentContextProvider;
    private IEnumerable<FieldDefinitionView> _fieldDefinitions = [];

    private DialogContext<CreateFieldDefinitionCommand> _createContext = new(() => { });

    public FieldDefinitionListingPage(
        IContentContextProvider contentContextProvider,
        IFieldDefinitionService fieldDefinitionService)
    {
        _contentContextProvider = contentContextProvider;
        
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
        if (CurrentOrganization is not null && CurrentHub is not null)
        {
            _fieldDefinitions = await FieldDefinitionService.GetFieldDefinitions(CurrentOrganization.Id, CurrentHub.Id);
        }
    }

    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new (
            CurrentOrganization!.Id,
            CurrentHub!.Id, 
            Guid.NewGuid()
        ));
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

    private IFieldDefinitionService FieldDefinitionService { get; }
}
