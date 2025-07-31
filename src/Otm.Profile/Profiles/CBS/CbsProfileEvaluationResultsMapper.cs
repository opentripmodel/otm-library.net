using System.Collections.Generic;
using System.Linq;
using Json.Schema;
using OpenTripModel.Profile.Internal;
using OpenTripModel.Validation;

namespace OpenTripModel.Profiles.CBS;

internal static class CbsProfileEvaluationResultsMapper
{
    public static ValidationResult Map(JsonSchema schema, EvaluationResults root)
    {
        // Collect error messages from tree
        var messages = new List<ValidationMessage>();

        // Flatten the tree into list
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
                    Severity = Severity.Error, // everything from JSON Schema is an error
                    Path     = instance,
                    Code     = code,
                    Message  = text,
                });
            }
        }


        return new ValidationResult(messages.ToArray());
    }

}
