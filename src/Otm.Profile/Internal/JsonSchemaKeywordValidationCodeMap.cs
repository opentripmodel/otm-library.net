using System.Collections.Generic;
using OpenTripModel.Validation;

namespace OpenTripModel.Profile.Internal;

internal static class JsonSchemaKeywordValidationCodeMap
{
    private static readonly Dictionary<string, ValidationCode> Map = new()
    {
        [""]                     = ValidationCode.AdditionalProperties,
        ["required"]             = ValidationCode.Required,
        ["type"]                 = ValidationCode.Type,
        ["enum"]                 = ValidationCode.Enum,
        ["minItems"]             = ValidationCode.MinItems,
        ["maxLength"]            = ValidationCode.MaxLength,
        ["minLength"]            = ValidationCode.MinLength,
        ["additionalProperties"] = ValidationCode.AdditionalProperties,
    };

    public static ValidationCode GetCode(string keyword)
    {
        // Empty key == false schema
        if (string.IsNullOrWhiteSpace(keyword))
            return ValidationCode.AdditionalProperties;

        return Map.TryGetValue(keyword, out var value) ? value : ValidationCode.GenericError;
    }

}
