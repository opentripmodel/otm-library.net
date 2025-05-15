namespace OpenTripModel.v5
{
    public abstract class UpdateEvent<TEntity>
        : Event
        where TEntity : IUpdateEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public override EventType EventType => EventType.UpdateEvent;



        /// <summary>
        /// The fields of the entity that need to be updated. All fields that are not present remain unchanged. 
        /// If you want to unset a field explicitly use null
        /// </summary>
        public virtual TEntity Entity { get; set; }
    }
}