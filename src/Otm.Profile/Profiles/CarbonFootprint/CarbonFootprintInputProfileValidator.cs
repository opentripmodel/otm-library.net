using System.Text.Json.Nodes;
using Json.Schema;
using OpenTripModel.Profiles.CarbonFootprint.Schema;
using OpenTripModel.Serialization;
using OpenTripModel.Validation;

namespace OpenTripModel.Profiles.CarbonFootprint;

/// <summary>
/// Validates a Trip entity as input for a CO₂ emission calculation, according to the
/// OTM Carbon Footprint profile (CO₂ Calculation Input). The trip must carry the transport
/// data (vehicle, load/unload stops, cargo) and energy data (energy consumption context
/// events) that a CO₂ calculation platform needs, as defined by the DALTI
/// "Uniforme kernset CO₂e".
/// </summary>
/// <remarks>
/// The Carbon Footprint profile is still a DRAFT; this validator follows the draft message
/// model and will be updated once the profile is formally released by the OTM working group.
/// </remarks>
public class CarbonFootprintInputProfileValidator
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

        var schema = CarbonFootprintSchemaProvider.InputSchema;

        var evaluationResults = schema.Evaluate(tripJsonRootNode, evalOptions);

        return CarbonFootprintEvaluationResultsMapper.Map(schema, evaluationResults);
    }
}
