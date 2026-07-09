using System.Text.Json.Nodes;
using Json.Schema;
using OpenTripModel.Profiles.CarbonFootprint.Schema;
using OpenTripModel.Serialization;
using OpenTripModel.Validation;

namespace OpenTripModel.Profiles.CarbonFootprint;

/// <summary>
/// Validates a Trip entity carrying calculated CO₂ emission results, according to the
/// OTM Carbon Footprint profile (CO₂ Calculation Result). The trip must carry at least one
/// emission context event with the total CO₂e emission and the calculation method details
/// (framework, ISO reference, allocation method, data quality), as defined by the DALTI
/// "Uniforme kernset CO₂e".
/// </summary>
/// <remarks>
/// The Carbon Footprint profile is still a DRAFT; this validator follows the draft message
/// model and will be updated once the profile is formally released by the OTM working group.
/// </remarks>
public class CarbonFootprintResultProfileValidator
    : IProfileValidator<OpenTripModel.v5.Trip>
{
    /// <inheritdoc />
    public ValidationResult Validate(OpenTripModel.v5.Trip trip)
    {
        // Serialize the trip first - JSON Schema validators work on JSON structures, not C# objects
        var serializer = new OtmSerializer();

        var serializedTrip = serializer.Serialize(trip);

        var tripJsonRootNode = JsonNode.Parse(serializedTrip);

        var evalOptions = new EvaluationOptions
        {
            OutputFormat = OutputFormat.List,
            PreserveDroppedAnnotations = true,
            AddAnnotationForUnknownKeywords = true,
            ValidateAgainstMetaSchema = true,
        };

        var schema = CarbonFootprintSchemaProvider.ResultSchema;

        var evaluationResults = schema.Evaluate(tripJsonRootNode, evalOptions);

        return CarbonFootprintEvaluationResultsMapper.Map(schema, evaluationResults);
    }
}
