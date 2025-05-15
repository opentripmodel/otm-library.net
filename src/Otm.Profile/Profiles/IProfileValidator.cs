using OpenTripModel.Validation;


namespace OpenTripModel.Profiles
{
    /// <summary>
    /// Defines a contract for validating a specific type of OTM entity against a profile.
    /// </summary>
    /// <typeparam name="T">The OTM entity type to validate.</typeparam>
    public interface IProfileValidator<in T>
    {
        /// <summary>
        /// Validates the specified entity and returns a result with any validation messages.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>A <see cref="ValidationResult"/> containing the validation outcome.</returns>
        ValidationResult Validate(T entity);
    }
}
