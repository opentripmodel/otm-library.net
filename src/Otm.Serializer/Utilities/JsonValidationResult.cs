using System;

namespace OpenTripModel.Utilities
{
    /// <summary>
    /// Represents the result of validating a JSON string, including success flag and error details.
    /// </summary>
    public class JsonValidationResult
    {
        /// <summary>
        /// Indicates whether the JSON was valid.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Descriptive error message if validation fails.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Optional inner exception if applicable.
        /// </summary>
        public Exception Exception { get; }

        private JsonValidationResult(bool isValid, string errorMessage = null, Exception exception = null)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        public static JsonValidationResult Success() => new JsonValidationResult(true);

        public static JsonValidationResult Failure(string message, Exception ex = null) =>
            new JsonValidationResult(false, message, ex);
    }
}
