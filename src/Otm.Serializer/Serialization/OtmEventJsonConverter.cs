using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenTripModel.v5;
using OpenTripModel.v5.Entities;

namespace OpenTripModel.Serialization
{
    /// <summary>
    /// Handles (de)serialization of the abstract <see cref="Event"/> type, e.g. inside
    /// <c>contextEvents</c>, using the OTM <c>eventType</c> field as discriminator. Without this
    /// converter the properties of concrete event types would be lost during serialization.
    /// </summary>
    public class OtmEventJsonConverter : JsonConverter<Event>
    {
        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert)
        {
            // Only intercept the abstract base type; concrete event types are handled by the
            // default object converter (which this converter delegates to).
            return typeToConvert == typeof(Event);
        }

        /// <inheritdoc />
        public override Event Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);

            var root = document.RootElement;

            if (!root.TryGetProperty("eventType", out var eventTypeProperty))
                throw new JsonException("Cannot deserialize event: required discriminator field 'eventType' is missing.");

            var eventType = eventTypeProperty.GetString();

            var concreteType = ResolveType(eventType)
                ?? throw new JsonException($"Cannot deserialize event: unsupported eventType '{eventType}'.");

            return (Event)root.Deserialize(concreteType, options);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Event value, JsonSerializerOptions options)
        {
            // Serialize using the runtime type so properties of the concrete event are included.
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }

        private static Type ResolveType(System.String eventType)
        {
            switch (eventType)
            {
                case "locationUpdateEvent":     return typeof(LocationUpdateEvent);
                case "fuelConsumedEvent":       return typeof(FuelConsumedEvent);
                case "emissionEvent":           return typeof(EmissionEvent);
                default:                        return null;
            }
        }
    }
}
