using System.Text.Json.Serialization;

namespace OpenTripModel.v5
{
    public class LatLonPointGeoReference
        : GeoReference
    {
        /// <summary>
        /// GeoReference discriminator. Fixed to <see cref="GeoReferenceType.LatLonPointGeoReference"/>.
        /// Ignored during serialization — the <c>type</c> JSON property is written by the polymorphic
        /// discriminator declared on <see cref="GeoReference"/>; re-applied here because
        /// <see cref="JsonIgnoreAttribute"/> does not inherit through property overrides.
        /// </summary>
        [JsonIgnore]
        public override GeoReferenceType Type => GeoReferenceType.LatLonPointGeoReference;


        /// <summary>
        /// The latitude of a point location.
        /// </summary>
        public System.Double Lat { get; set; }


        /// <summary>
        /// The longitude of a point location.
        /// </summary>
        public System.Double Lon { get; set; }


        /// <summary>
        /// The speed of a moving vehicle.
        /// </summary>
        public UnitWithValue Speed { get; set; }


        /// <summary>
        /// The heading of a vehicle, that is: the direction the "nose" of the vehicle is pointing to.
        /// </summary>
        public UnitWithValue Heading { get; set; }


        /// <summary>
        /// The bearing of a vehicle, that is: the angle between the vehicle and its destination. 
        /// Either measured relative or absolute. See Wikipedia for an explanation.
        /// </summary>
        public UnitWithValue Bearing { get; set; }


        /// <summary>
        /// Denotes how the bearing is measured.
        /// </summary>
        public BearingType? BearingType { get; set; }
    }
}
