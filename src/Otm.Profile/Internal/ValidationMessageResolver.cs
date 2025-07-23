using System;
using System.Collections.Generic;
using OpenTripModel.Validation;

namespace OpenTripModel.Profile.Internal;

internal static class ValidationMessageResolver
{
    private static readonly IReadOnlyDictionary<ValidationCode, Func<string, string, string>> Map =
        new Dictionary<ValidationCode, Func<string, string, string>>
        {
            [ValidationCode.Enum] = (_, _) => TextResources.UserFriendlyOverrides.EnumMessage,

            [ValidationCode.Required] = (raw, _) =>
                RegexRegistry.MissingRequiredProperty
                    .Replace(raw, m => $"\"{m.Groups[1].Value}\"")
                    .Replace(TextResources.JsonSchemaTokens.RequiredSuffix, TextResources.UserFriendlyOverrides.RequiredSuffix),

            [ValidationCode.AdditionalProperties] = (_, path) =>
                string.Format(
                    TextResources.UserFriendlyOverrides.AdditionalTemplate,
                    RegexRegistry.JsonPointerLastToken.Match(path).Value
                ),

            [ValidationCode.Type] = (raw, _) =>
                raw
                    .Replace(TextResources.JsonSchemaTokens.ValueIsPrefix, TextResources.UserFriendlyOverrides.ActualTypePrefix)
                    .Replace(TextResources.JsonSchemaTokens.ButShouldBeConnector, TextResources.UserFriendlyOverrides.ExpectedConnector)
        };

    public static string GetMessage(ValidationCode code, string raw, string path)
    {
        return Map.TryGetValue(code, out var formatter) ? formatter(raw, path) : raw;
    }

}
