namespace OpenTripModel.v5
{
    public class StopUpdateEventEntity
        : StopAction, IUpdateEvent
    {
        public EntityType EntityType => EntityType.Stop;
    }
}
