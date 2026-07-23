using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenTripModel.Profiles.CarbonFootprint;

/// <summary>
/// Serializer options tuned for the OTM <c>Carbon Footprint (input)</c> profile. The profile's
/// schema uses <c>additionalProperties: false</c> at every level, so we must avoid emitting
/// default-valued properties that the profile does not define (e.g. <c>Trip.Status</c>,
/// <c>Event.Lifecycle</c>).
///
/// The toolkit-wide <see cref="Serialization.JsonOptionsFactory"/> combines
/// <see cref="JsonIgnoreCondition.WhenWritingNull"/> and <see cref="JsonIgnoreCondition.WhenWritingDefault"/>
/// with a bitwise OR; because <see cref="JsonIgnoreCondition"/> is not a flags enum, that expression
/// resolves to <see cref="JsonIgnoreCondition.WhenWritingNull"/> only, which lets zero-valued enums
/// leak into the JSON. Here we use <see cref="JsonIgnoreCondition.WhenWritingDefault"/> on its own —
/// it is a superset that covers both nulls and default value-type values — so serialized JSON
/// contains only the properties the caller explicitly populated.
/// </summary>
internal static class CarbonFootprintInputSerializerOptions
{
    public static JsonSerializerOptions Create()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            },
        };
    }
}
