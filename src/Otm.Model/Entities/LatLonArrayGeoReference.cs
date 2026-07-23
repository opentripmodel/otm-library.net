using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenTripModel.v5
{
    public class LatLonArrayGeoReference
        : GeoReference
    {
        [JsonIgnore]
        public override GeoReferenceType Type => GeoReferenceType.LatLonArrayGeoReference;

        /// <summary>
        /// An array of lat/lon points.
        /// </summary>
        public IEnumerable<LatLonPointGeoReference> Points { get; set; }
    }
}
