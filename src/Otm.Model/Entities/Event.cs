using System.Collections.Generic;
using System.Text.Json.Serialization;
using OpenTripModel.v5.Entities;

namespace OpenTripModel.v5
{
    /// <summary>
    /// Abstract base type for events that can be attached to an entity's <see cref="OtmEntity.ContextEvents"/>.
    /// Polymorphic serialization is driven by the <c>eventType</c> discriminator property, which is written by
    /// <see cref="System.Text.Json"/> according to the registered <see cref="JsonDerivedTypeAttribute"/> entries.
    /// The <see cref="EventType"/> getter on derived classes is ignored by the serializer to avoid writing the
    /// property twice; its value is still available for programmatic use.
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "eventType")]
    [JsonDerivedType(typeof(FuelConsumedEvent), "fuelConsumedEvent")]
    [JsonDerivedType(typeof(LocationUpdateEvent), "locationUpdateEvent")]
    public abstract class Event
        : OtmEntity

    {
        /// <summary>
        /// The concrete event type. Not serialized directly; the <c>eventType</c> JSON property is written by the
        /// polymorphic type discriminator. Kept as an abstract getter so derived types declare their identity and
        /// so callers can branch on it without reflection.
        /// </summary>
        [JsonIgnore]
        public abstract EventType EventType { get; }



        /// <summary>
        /// A lifecycle models when the data in the action is taking place. You can provide the same action in multiple
        /// lifecycles to model how it changes over time. For example the planned and realized time of an action taking
        /// place can differ because of unforseen circumstances (such as traffic jams).
        /// </summary>
        public Lifecycle Lifecycle { get; set; }



        /// <summary>
        /// The actors associated with this transport order, for instance the consignor, consignee.
        /// </summary>
        public IEnumerable<InlineAssociationActorType> Actors { get; set; }
    }
}
