namespace OpenTripModel.v5
{
    public class ConsignmentUpdateEvent
        : UpdateEvent<ConsignmentUpdateEventEntity>
    {
        public EntityType EntityType => EntityType.Consignment;
    }
}
