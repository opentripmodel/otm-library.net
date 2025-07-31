using System.Text.Json.Serialization;

namespace OpenTripModel.v5
{
    [JsonDerivedType(typeof(AddressGeoReference))]
    [JsonDerivedType(typeof(LatLonPointGeoReference))]
    [JsonDerivedType(typeof(LatLonArrayGeoReference))]
    public abstract class GeoReference
    {
        public abstract GeoReferenceType Type { get; }
    }
}
