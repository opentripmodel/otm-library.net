using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override string ToString()
        {
            if (IsValid)
                return "Validation succeeded (0 errors)";

            // Headline
            var sb = new StringBuilder()
                .AppendLine($"Validation failed ({Messages.Count} issues):");

            foreach (var msg in Messages.OrderBy(m => m.Severity))
            {
                sb.Append('[')
                    .Append(msg.Severity)
                    .Append("] ")
                    .Append(msg.Path)
                    .Append(" → ")
                    .AppendLine(msg.Message);
            }

            return sb.ToString().TrimEnd();
        }



        /// <summary>
        /// Gets whether the validation is considered successful (no errors).
        /// </summary>
        public bool IsValid => this.Messages.All(m => m.Severity != Severity.Error);



        /// <summary>
        /// Gets the list of validation messages (errors, warnings, infos).
        /// </summary>
        public List<ValidationMessage> Messages { get; } = new List<ValidationMessage>();



        public static ValidationResult Success() => new ValidationResult();



        public static ValidationResult Failure(string message ) =>
            new( new ValidationMessage { Severity = Severity.Error, Message = message } );
    }
}
