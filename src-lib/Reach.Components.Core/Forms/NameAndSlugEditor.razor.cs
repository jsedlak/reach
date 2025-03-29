using Microsoft.AspNetCore.Components;
using Tazor.Extensions;

namespace Reach.Components.Forms;

/// <summary>
/// Provides a common pattern for rendering a name and slug combination editor
/// </summary>
public partial class NameAndSlugEditor : ComponentBase
{
    private bool _isSlugEditorVisible = false;
    private string _slugCancelValue = "";
    private bool _isValidating = false;
    private Func<string, string, Task<bool>> _validationCallback;

    public NameAndSlugEditor()
    {
        _validationCallback = (__name, __slug) => Task.FromResult(
            !string.IsNullOrWhiteSpace(__name) && !string.IsNullOrWhiteSpace(__slug)
        );
    }

    private async Task OnNameChanged(string value)
    {
        var areNameAndSlugAttached = 
            (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Slug)) ||
            Slugger.Slugify(Name).Equals(Slug, StringComparison.OrdinalIgnoreCase);

        Name = value;

        if (NameChanged.HasDelegate)
        {
            await NameChanged.InvokeAsync(Name);
        }

        if (areNameAndSlugAttached)
        {
            Slug = Slugger.Slugify(value);

            if (SlugChanged.HasDelegate)
            {
                await SlugChanged.InvokeAsync(Slug);
            }
        }

        await ValidateData();
    }

    private async Task ValidateData()
    {
        _isValidating = true;
        StateHasChanged();

        var newValidValue = await Validate(Name, Slug);
        IsValid = newValidValue;

        if(IsValidChanged.HasDelegate)
        {
            await IsValidChanged.InvokeAsync(IsValid);
        }

        _isValidating = false;
        StateHasChanged();
    }

    #region Slug Editing
    private async Task OnSlugChanged(string newSlug)
    {
        Slug = Slugger.Slugify(newSlug);

        if(SlugChanged.HasDelegate)
        {
            await SlugChanged.InvokeAsync(Slug);
        }

        await ValidateData();
    }

    protected virtual async Task OnCancelSlugEdit()
    {
        Slug = _slugCancelValue;

        if(SlugChanged.HasDelegate)
        {
            await SlugChanged.InvokeAsync(Slug);
        }

        _isSlugEditorVisible = false;
        StateHasChanged();

        await ValidateData();
    }

    protected virtual async Task OnConfirmSlugEdit()
    {
        _isSlugEditorVisible = false;
        StateHasChanged();

        await ValidateData();
    }

    protected virtual void OnBeginEditSlug()
    {
        _slugCancelValue = Slug;
        _isSlugEditorVisible = true;
        StateHasChanged();
    }
    #endregion

    [Parameter] public string Name { get; set; } = null!;
    
    [Parameter] public EventCallback<string> NameChanged { get; set; }

    [Parameter] public string Slug { get; set; } = null!;
    
    [Parameter] public EventCallback<string> SlugChanged { get; set; }

    [Parameter] public bool IsValid { get; set; }

    [Parameter]public EventCallback<bool> IsValidChanged { get; set; }

    [Parameter]
    public Func<string, string, Task<bool>> Validate
    {
        get { return _validationCallback; }
        set { _validationCallback = value; }
    }
}