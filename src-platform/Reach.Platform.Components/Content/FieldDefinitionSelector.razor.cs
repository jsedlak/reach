using Microsoft.AspNetCore.Components;
using Reach.Content.Views;
using Reach.Platform.Services;
using Tazor.Components.Theming;

namespace Reach.Platform.Components.Content;

public partial class FieldDefinitionSelector : BaseReachComponent
{
    private readonly IThemeManager _themeManager;
    private readonly IFieldDefinitionService _fieldDefinitionService;
    private IEnumerable<FieldDefinitionView> _fieldDefinitions = [];

    public FieldDefinitionSelector(IThemeManager themeManager, IFieldDefinitionService fieldDefinitionService)
    {
        _themeManager = themeManager;
        _fieldDefinitionService = fieldDefinitionService;
    }

    private async Task OnFieldDefinitionChanged(ChangeEventArgs args)
    {
        if (Guid.TryParse(args.Value?.ToString() ?? Guid.Empty.ToString(), out var newValue))
        {
            SelectedFieldDefinitionId = newValue;
            await SelectedFieldDefinitionIdChanged.InvokeAsync(SelectedFieldDefinitionId);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        if (IsReady)
        {
            _fieldDefinitions = await _fieldDefinitionService.GetFieldDefinitons(
                CurrentOrganizationId.GetValueOrDefault(),
                CurrentHubId.GetValueOrDefault()
            ) ?? [];
        }
    }

    protected override async Task OnComponentIsReady()
    {
        _fieldDefinitions = await _fieldDefinitionService.GetFieldDefinitons(
            CurrentOrganizationId.GetValueOrDefault(),
            CurrentHubId.GetValueOrDefault()
        ) ?? [];
    }

    [Parameter]
    public Guid SelectedFieldDefinitionId { get; set; }

    [Parameter]
    public EventCallback<Guid> SelectedFieldDefinitionIdChanged { get; set; }
}
