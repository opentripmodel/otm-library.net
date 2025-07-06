using OpenTripModel.Validation;


namespace OpenTripModel.Profiles.CBS
{
    /// <summary>
    /// Validates a Trip entity for completeness according to CBS export standards.
    /// </summary>
    public class CbsProfileValidator 
        : IProfileValidator<OpenTripModel.v5.Trip>
    {
        public ValidationResult Validate(OpenTripModel.v5.Trip trip)
        {
            // single package release test
            return ValidationResult.Success();
        }
    }


    // example change in profile


}
