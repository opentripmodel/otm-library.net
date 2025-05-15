namespace OpenTripModel.v5
{
    public class StopUpdateEvent
        : UpdateEvent<StopUpdateEventEntity>, IUpdateEvent
    {
        public EntityType EntityType => EntityType.Stop;
    }
}
