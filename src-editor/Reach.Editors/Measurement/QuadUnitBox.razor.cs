using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Reach.Editors.Measurement;

public partial class QuadUnitBox : BaseUnitEditor
{
    private QuadUnitValue _value = new();

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        try
        {
            _value = JsonSerializer.Deserialize<QuadUnitValue>(Value) ?? new();
        }
        catch
        {
            /* Log? No-op? */
        }
    }

    private async Task HandleValue1Changed(string newValue)
    {
        _value.Value1 = newValue;

        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private async Task HandleValue2Changed(string newValue)
    {
        _value.Value2 = newValue;

        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private async Task HandleValue3Changed(string newValue)
    {
        _value.Value3 = newValue;

        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private async Task HandleValue4Changed(string newValue)
    {
        _value.Value4 = newValue;

        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private async Task HandleUnitChanged(ChangeEventArgs args)
    {
        _value.Unit = args.Value?.ToString() ?? string.Empty;

        await base.OnValueChanged(JsonSerializer.Serialize(_value));
    }

    private class QuadUnitValue : BaseUnitValue
    {
        private string _value1 = string.Empty;
        private string _value2 = string.Empty;
        private string _value3 = string.Empty;
        private string _value4 = string.Empty;

        protected override void UpdateDisplayValue()
        {
            if (string.IsNullOrWhiteSpace(_value1) ||
                string.IsNullOrWhiteSpace(_value2) || 
                string.IsNullOrWhiteSpace(_value3) ||
                string.IsNullOrWhiteSpace(_value4) ||
                string.IsNullOrWhiteSpace(Unit))
            {
                DisplayValue = null;
                DisplayValue1 = null;
                DisplayValue2 = null;
                DisplayValue3 = null;
                DisplayValue4 = null;
            }
            else
            {
                DisplayValue = $"{_value1}{Unit} {_value2}{Unit} {_value3}{Unit} {_value4}{Unit}";
                DisplayValue1 = $"{_value1}{Unit}";
                DisplayValue2 = $"{_value2}{Unit}";
                DisplayValue3 = $"{_value3}{Unit}";
                DisplayValue4 = $"{_value4}{Unit}";
            }
        }

        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                UpdateDisplayValue();
            }
        }

        public string Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                UpdateDisplayValue();
            }
        }

        public string Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
                UpdateDisplayValue();
            }
        }

        public string Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
                UpdateDisplayValue();
            }
        }

        public string? DisplayValue { get; set; } = null;

        public string? DisplayValue1 { get; set; } = null;

        public string? DisplayValue2 { get; set; } = null;


        public string? DisplayValue3 { get; set; } = null;


        public string? DisplayValue4 { get; set; } = null;
    }
}
