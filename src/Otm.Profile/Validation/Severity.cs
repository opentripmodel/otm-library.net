namespace OpenTripModel.Validation
{
    /// <summary>
    /// Specifies the severity level of a validation message.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Informational message that does not affect validity.
        /// </summary>
        Info,

        /// <summary>
        /// Indicates a potential issue that may require attention.
        /// </summary>
        Warning,

        /// <summary>
        /// Indicates a critical issue that renders the entity invalid.
        /// </summary>
        Error
    }
}
