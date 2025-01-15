namespace Reach.Editors.Simple;

public partial class BooleanEditor : BaseEditor
{
    private bool _value = false;

    protected override Task OnValueSetAsync(string value)
    {
        if (Boolean.TryParse(value, out var newValue))
        {
            _value = newValue;
        }

        return Task.CompletedTask;
    }
}
