namespace OpenTripModel.Validation;

/// <summary>
/// Identifies the JSON‑Schema rule that triggered a
/// <see cref="ValidationMessage"/>.
/// Each value maps 1‑to‑1 to a keyword in the Draft‑2020‑12 specification.
/// </summary>
public enum ValidationCode
{
    /// <summary>
    /// A property declared as <c>required</c> is missing from the instance.
    /// </summary>
    Required,

    /// <summary>
    /// The JSON value’s data‑type does not match the schema’s <c>type</c> constraint.
    /// </summary>
    Type,

    /// <summary>
    /// The value is not one of the permitted options listed under <c>enum</c>.
    /// </summary>
    Enum,

    /// <summary>
    /// An array has fewer elements than the minimum specified by <c>minItems</c>.
    /// </summary>
    MinItems,

    /// <summary>
    /// A string exceeds the maximum length allowed by <c>maxLength</c>.
    /// </summary>
    MaxLength,

    /// <summary>
    /// A string is shorter than the minimum length required by <c>minLength</c>.
    /// </summary>
    MinLength,

    /// <summary>
    /// The instance contains a member that is disallowed because
    /// <c>additionalProperties</c> is set to <c>false</c>.
    /// </summary>
    AdditionalProperties,

    /// <summary>
    /// Fallback code used when the mapper cannot translate a keyword
    /// to a dedicated member.
    /// </summary>
    GenericError
}
