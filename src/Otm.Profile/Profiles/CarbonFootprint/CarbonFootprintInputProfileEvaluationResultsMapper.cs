using System.Collections.Generic;
using System.Linq;
using Json.Schema;
using OpenTripModel.Profile.Internal;
using OpenTripModel.Validation;

namespace OpenTripModel.Profiles.CarbonFootprint;

/// <summary>
/// Maps the evaluation output produced by <see cref="JsonSchema.Evaluate"/> into the
/// toolkit-wide <see cref="ValidationResult"/> shape (flattened error list with JSON pointers
/// and human-readable messages). Mirrors the CBS profile mapper so that both profiles expose
/// identical result semantics.
/// </summary>
internal static class CarbonFootprintInputProfileEvaluationResultsMapper
{
    public static ValidationResult Map(JsonSchema schema, EvaluationResults root)
    {
        var messages = new List<ValidationMessage>();

        // Flatten the evaluation tree (side-effect: populates root.Details when OutputFormat.List is used)
        root.ToList();

        var errorNodes = root.Details.Where(n => n.HasErrors);

        foreach (var errorNode in errorNodes)
        {
            foreach (var keywordErrorMap in errorNode.Errors!)
            {
                var code = JsonSchemaKeywordValidationCodeMap.GetCode(keywordErrorMap.Key);

                var instance = errorNode.InstanceLocation.ToString();

                var text = ValidationMessageResolver.GetMessage(schema, errorNode, code, keywordErrorMap.Value, instance);

                messages.Add(new ValidationMessage
                {
                    Severity = Severity.Error,
                    Path     = instance,
                    Code     = code,
                    Message  = text,
                });
            }
        }

        return new ValidationResult(messages.ToArray());
    }
}
