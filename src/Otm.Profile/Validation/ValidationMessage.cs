namespace OpenTripModel.Validation
{
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
        /// The human-readable message.
        /// </summary>
        public System.String Message { get; set; }



        /// <summary>
        /// Optional path or property reference indicating the source of the issue.
        /// </summary>
        public System.String Path { get; set; }
    }
}
