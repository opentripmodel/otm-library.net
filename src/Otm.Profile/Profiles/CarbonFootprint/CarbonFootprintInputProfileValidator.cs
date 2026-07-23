using System.Text.Json.Nodes;
using Json.Schema;
using OpenTripModel.Profiles.CarbonFootprint.Schema;
using OpenTripModel.Serialization;
using OpenTripModel.Validation;

namespace OpenTripModel.Profiles.CarbonFootprint;

/// <summary>
/// Validates a <see cref="OpenTripModel.v5.Trip"/> against the OTM <c>Carbon Footprint (input)</c>
/// profile (v0.5). This profile describes the shape of a Trip carrying <c>fuelConsumedEvent</c>
/// context events and is typically produced by a TMS and consumed by a CO2e calculation platform.
/// </summary>
public class CarbonFootprintInputProfileValidator
    : IProfileValidator<OpenTripModel.v5.Trip>
{
    /// <summary>
    /// A serializer configured with the profile-strict options
    /// (<see cref="CarbonFootprintInputSerializerOptions"/>). Re-exposed so callers that want the
    /// exact JSON the validator will evaluate (e.g. for transport to a CO2e calculator) can share
    /// the same configuration.
    /// </summary>
    public static OtmSerializer CreateSerializer() =>
        new(CarbonFootprintInputSerializerOptions.Create());


    /// <inheritdoc />
    public ValidationResult Validate(OpenTripModel.v5.Trip trip)
    {
        // Serialize to JSON first — JSON Schema validators operate on JSON structures, not C# objects.
        // We use a profile-strict serializer so zero-valued enums (e.g. Trip.Status = Requested = 0)
        // are not emitted and therefore do not trip additionalProperties=false in the schema.
        var serializer = CreateSerializer();

        var serializedTrip = serializer.Serialize(trip);

        var tripJsonRootNode = JsonNode.Parse(serializedTrip);

        var evalOptions = new EvaluationOptions
        {
            OutputFormat = OutputFormat.List,
            PreserveDroppedAnnotations = true,
            AddAnnotationForUnknownKeywords = true,
            ValidateAgainstMetaSchema = true,
        };

        var schema = CarbonFootprintInputSchemaProvider.CreateCustomJsonSchema();

        var evaluationResults = schema.Evaluate(tripJsonRootNode, evalOptions);

        return CarbonFootprintInputProfileEvaluationResultsMapper.Map(schema, evaluationResults);
    }
}
