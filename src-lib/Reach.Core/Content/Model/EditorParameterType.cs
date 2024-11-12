namespace Reach.Content.Model;

/// <summary>
/// Provides basic parameter type information
/// </summary>
public enum EditorParameterType
{
    /// <summary>
    /// Render the parameter as a textbox
    /// </summary>
    Text = 0,

    /// <summary>
    /// Render the parameter as a number
    /// </summary>
    Number = 1,

    /// <summary>
    /// Render as a date
    /// </summary>
    Date = 2,

    /// <summary>
    /// Render as a date and time
    /// </summary>
    DateTime = 3,

    /// <summary>
    /// Render as a checkbox
    /// </summary>
    Boolean = 4
}