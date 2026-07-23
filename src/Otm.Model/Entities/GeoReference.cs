using System.Text.Json.Serialization;

namespace OpenTripModel.v5
{
    /// <summary>
    /// Abstract base for geographic references. Polymorphic serialization is driven by the
    /// <c>type</c> discriminator property, which STJ writes automatically from the registered
    /// <see cref="JsonDerivedTypeAttribute"/> entries and uses to pick the correct concrete type
    /// when deserializing. The derived classes' <see cref="Type"/> override is marked
    /// <see cref="JsonIgnoreAttribute"/> so the property is not written twice.
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(AddressGeoReference), "addressGeoReference")]
    [JsonDerivedType(typeof(LatLonPointGeoReference), "latLonPointGeoReference")]
    [JsonDerivedType(typeof(LatLonArrayGeoReference), "latLonArrayGeoReference")]
    public abstract class GeoReference
    {
        /// <summary>
        /// The concrete geo-reference type. Not serialized directly; the <c>type</c> property is
        /// emitted by the polymorphic type discriminator.
        /// </summary>
        [JsonIgnore]
        public abstract GeoReferenceType Type { get; }
    }
}
