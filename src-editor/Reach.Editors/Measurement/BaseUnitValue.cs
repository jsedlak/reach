namespace Reach.Editors.Measurement;

public abstract class BaseUnitValue
{
    private string _unit = string.Empty;

    protected abstract void UpdateDisplayValue();

    public string Unit
    {
        get { return _unit; }
        set
        {
            _unit = value;
            UpdateDisplayValue();
        }
    }
}