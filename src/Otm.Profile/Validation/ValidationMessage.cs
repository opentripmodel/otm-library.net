namespace OpenTripModel.Validation;

/// <summary>
/// Represents a single validation message with severity and location context.
/// </summary>
public class ValidationMessage
{
    /// <summary>
    /// The severity of the message (Info, Warning, Error).
    /// </summary>
    public Severity Severity { get; set; }


    /// <summary>
    /// Identifier for the failed rule; useful for filtering.
    /// </summary>
    public ValidationCode Code { get; set; }


    /// <summary>
    /// The human-readable message.
    /// </summary>
    public string Message { get; set; }


    /// <summary>
    /// Optional path or property reference indicating the source of the issue.
    /// </summary>
    public string Path { get; set; }
}
