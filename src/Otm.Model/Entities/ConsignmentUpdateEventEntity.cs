namespace OpenTripModel.v5
{
    public class ConsignmentUpdateEventEntity
        : Consignment, IUpdateEvent
    {
        public EntityType EntityType => EntityType.Consignment;
    }
}
