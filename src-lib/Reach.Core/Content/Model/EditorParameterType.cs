namespace Reach.Content.Model;

/// <summary>
/// Provides basic parameter type information
/// </summary>
public enum EditorParameterType
{
    /// <summary>
    /// Render the parameter as a textbox
    /// </summary>
    Text,

    /// <summary>
    /// Render the parameter as a number
    /// </summary>
    Number,

    /// <summary>
    /// Render as a date
    /// </summary>
    Date,

    /// <summary>
    /// Render as a date and time
    /// </summary>
    DateTime,

    /// <summary>
    /// Render as a checkbox
    /// </summary>
    Boolean
}