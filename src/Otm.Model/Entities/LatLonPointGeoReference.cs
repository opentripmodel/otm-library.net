namespace OpenTripModel.v5
{
    public class LatLonPointGeoReference
        : GeoReference
    {
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
        public BearingType BearingType { get; set; }
    }
}
