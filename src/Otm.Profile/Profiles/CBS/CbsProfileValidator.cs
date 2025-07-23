using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Json.Schema;
using OpenTripModel.Profiles.CBS.Schema;
using OpenTripModel.Serialization;
using OpenTripModel.Validation;


namespace OpenTripModel.Profiles.CBS;

/// <summary>
/// Validates a Trip entity for completeness according to CBS export standards.
/// </summary>
public class CbsProfileValidator
    : IProfileValidator<OpenTripModel.v5.Trip>
{
    public ValidationResult Validate(OpenTripModel.v5.Trip trip)
    {
        // Serialize the trip first - JSON Schema validators work on JSON structures, not C# objects
        var serializer = new OtmSerializer();

        var serializedTrip = serializer.Serialize(trip);

        var tripAsRoot = JsonNode.Parse(serializedTrip);


        // Evaluate on Open-source CBS Schema
        var cbsSchemaEvaluationResults = CbsSchemaProvider.GetSchemaWithMultipleLocations().Evaluate(tripAsRoot, new EvaluationOptions
        {
            OutputFormat = OutputFormat.List,
            PreserveDroppedAnnotations = true,
            AddAnnotationForUnknownKeywords = true,
            ValidateAgainstMetaSchema = true,
        });


        var validationResult = CbsProfileEvaluationResultsMapper.Map(cbsSchemaEvaluationResults);

        return validationResult;
    }
}
