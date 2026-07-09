using System.Collections.Generic;
using System.Linq;
using Json.Schema;
using OpenTripModel.Profile.Internal;
using OpenTripModel.Validation;

namespace OpenTripModel.Profiles.CarbonFootprint;

internal static class CarbonFootprintEvaluationResultsMapper
{
    public static ValidationResult Map(JsonSchema schema, EvaluationResults root)
    {
        // The list output also contains failed evaluations of alternative branches (`contains`,
        // `if`) that did not make the document invalid - don't report anything when the
        // document as a whole is valid.
        if (root.IsValid)
            return ValidationResult.Success();

        // Collect error messages from tree
        var messages = new List<ValidationMessage>();

        // Flatten the tree into list
        root.ToList();

        var errorNodes = root.Details.Where(n => n.HasErrors && !IsBranchDiagnostic(n));

        foreach (var errorNode in errorNodes)
        {
            foreach (var keywordErrorMap in errorNode.Errors!)
            {
                var code = JsonSchemaKeywordValidationCodeMap.GetCode(keywordErrorMap.Key);

                var instance = errorNode.InstanceLocation.ToString();

                var text = ValidationMessageResolver.GetMessage(schema, errorNode, code, keywordErrorMap.Value, instance);

                messages.Add(new ValidationMessage
                {
                    Severity = Severity.Error, // everything from JSON Schema is an error
                    Path     = instance,
                    Code     = code,
                    Message  = text,
                });
            }
        }


        return new ValidationResult(messages.ToArray());
    }


    /// <summary>
    /// Errors produced while probing a `contains` or `if` branch are diagnostics of a branch
    /// that simply did not match; the meaningful error (e.g. "must contain a matching item")
    /// is reported on the array/object node itself.
    /// </summary>
    private static bool IsBranchDiagnostic(EvaluationResults node)
    {
        var path = node.EvaluationPath.ToString();

        return path.Contains("/contains/")
               || path.Contains("/if/")
               || path.EndsWith("/if");
    }
}
