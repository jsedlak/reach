namespace Reach.Editors.Simple;

public partial class BooleanEditor : BaseEditor
{
    private bool _valueAsBoolean = false;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (bool.TryParse(Value, out var newVal))
        {
            _valueAsBoolean = newVal;
        }
    }

    private async Task OnCheckedChanged(bool newValue)
    {
        await base.OnValueChanged(newValue.ToString());
    }
}
