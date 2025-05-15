namespace OpenTripModel.v5
{
    public class TripUpdateEventEntity
    : v5.Trip, IUpdateEvent
    {
        public EntityType EntityType => EntityType.Trip;
    }
}