using OpenTripModel.Validation;

namespace OpenTripModel.Profiles
{
    /// <summary>
    /// Validates a TransportOrder entity against the default completeness rules.
    /// </summary>
    public class TransportOrderProfileValidator 
        : IProfileValidator<v5.TransportOrder>
    {
        public ValidationResult Validate(v5.TransportOrder order)
        {
            return ValidationResult.Failure("Not implemented.");
        }
    }
}
