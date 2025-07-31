using System;
using System.Collections.Generic;
using Json.Schema;
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

    public static string GetMessage(JsonSchema rootSchema, EvaluationResults errorNode, ValidationCode code, string raw, string path)
    {
        if (code == ValidationCode.Enum)
            return BuildEnumMessage(rootSchema, errorNode);

        return Map.TryGetValue(code, out var formatter) ? formatter(raw, path) : raw;
    }


    private static string BuildEnumMessage(
        JsonSchema rootSchema,
        EvaluationResults errorNode
    )
    {
        const string baseText = TextResources.UserFriendlyOverrides.EnumMessage;

        var allowed = JsonSchemaEnumValues.Get(rootSchema, errorNode.SchemaLocation.Fragment.TrimStart('#'));

        return allowed.Count == 0
            ? baseText
            : $"{baseText}: {string.Join(", ", allowed)}";
    }

}
