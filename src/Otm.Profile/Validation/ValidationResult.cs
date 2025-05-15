using System.Collections.Generic;
using System.Linq;

namespace OpenTripModel.Validation
{
    /// <summary>
    /// Represents the result of a profile validation, including success flag and message list.
    /// </summary>
    public class ValidationResult
    {
        public ValidationResult() { }


        public ValidationResult( params ValidationMessage[] validationMessages )
        {
            if (validationMessages != null && validationMessages.Length > 0 )
                this.Messages.AddRange(validationMessages);
        }



        /// <summary>
        /// Gets whether the validation is considered successful (no errors).
        /// </summary>
        public System.Boolean IsValid => !this.Messages.Any(m => m.Severity == Severity.Error);



        /// <summary>
        /// Gets the list of validation messages (errors, warnings, infos).
        /// </summary>
        public List<ValidationMessage> Messages { get; } = new List<ValidationMessage>();



        public static ValidationResult Success() => new ValidationResult();



        public static ValidationResult Failure(System.String message ) =>
            new ValidationResult( new ValidationMessage() { Severity = Severity.Error, Message = message } );
    }
}
