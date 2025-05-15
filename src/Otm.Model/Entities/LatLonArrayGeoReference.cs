using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class LatLonArrayGeoReference
        : GeoReference
    {
        public override GeoReferenceType Type => GeoReferenceType.LatLonArrayGeoReference;

        /// <summary>
        /// An array of lat/lon points.
        /// </summary>
        public IEnumerable<LatLonPointGeoReference> Points { get; set; }
    }
}
