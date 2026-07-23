using System.Text.Json.Serialization;

namespace OpenTripModel.v5
{
    /// <summary>
    /// A context event that records the amount of a particular fuel consumed during a trip or action.
    /// This is the event type carried by the <c>Carbon Footprint (input)</c> OTM profile, which feeds
    /// downstream CO2e calculation platforms.
    /// </summary>
    public class FuelConsumedEvent
        : Event
    {
        /// <summary>
        /// Event discriminator. Fixed to <see cref="v5.EventType.FuelConsumedEvent"/>.
        /// Marked <see cref="JsonIgnoreAttribute"/> because STJ writes the <c>eventType</c> JSON property
        /// via the polymorphic type discriminator declared on the <see cref="Event"/> base class; the
        /// attribute does not propagate through property overrides, so it is re-applied here.
        /// </summary>
        [JsonIgnore]
        public override EventType EventType => EventType.FuelConsumedEvent;


        /// <summary>
        /// The type of fuel consumed in this event.
        /// </summary>
        public Fuel Fuel { get; set; }


        /// <summary>
        /// The amount of fuel consumed in this event, expressed as a value with its unit
        /// (for example <c>50 l</c>, <c>12 kg</c>, or <c>100 kWh</c>).
        /// </summary>
        public UnitWithValue Amount { get; set; }


        /// <summary>
        /// The consignment this fuel consumption pertains to, when allocation of fuel to a specific
        /// consignment is known. Leave null for trip-level (un-allocated) consumption.
        /// </summary>
        public InlineAssociationType<Consignment> Consignment { get; set; }
    }
}
