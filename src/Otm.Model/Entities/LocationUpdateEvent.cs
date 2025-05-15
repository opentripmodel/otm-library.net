namespace OpenTripModel.v5.Entities
{
    public class LocationUpdateEvent
        : Event
    {
        public override EventType EventType => EventType.LocationUpdateEvent;

        public InlineAssociationType<Vehicle> Vehicle { get; set; }


        public LatLonPointGeoReference GeoReference { get; set; }
    }
}
