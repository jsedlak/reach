using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Reach.Editors.Measurement;

public partial class SingleUnitBox : BaseUnitEditor
{
    private SingleUnitValue _value = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        try
        {
            _value = JsonSerializer.Deserialize<SingleUnitValue>(Value) ?? new();
        }
        catch
        {
            /* Log? No-op? */
        }
    }

    private async Task HandleValueChanged(string newValue)
    {
        _value.Value = newValue;

        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private async Task HandleUnitChanged(ChangeEventArgs args)
    {
        _value.Unit = args.Value?.ToString() ?? string.Empty;
        
        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private class SingleUnitValue : BaseUnitValue
    {
        private string _value = string.Empty;

        protected override void UpdateDisplayValue()
        {
            if(string.IsNullOrWhiteSpace(_value) || string.IsNullOrWhiteSpace(Unit))
            {
                DisplayValue = null;
            }
            else
            {
                DisplayValue = $"{_value}{Unit}";
            }
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                UpdateDisplayValue();
            }
        }

        public string? DisplayValue { get; set; } = null;
    }
}
