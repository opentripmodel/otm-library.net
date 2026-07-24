namespace OpenTripModel.v5
{
    /// <summary>
    /// A move action: the drive between two stops. Distinct from <see cref="StopAction"/> so that it
    /// serializes with <c>actionType: "move"</c> — the discriminator is derived from the concrete type,
    /// so a stop with its <see cref="Action.ActionType"/> merely set to <see cref="ActionType.Move"/>
    /// would still serialize as a stop.
    /// </summary>
    public class MoveAction
        : Action
    {
        public override ActionType ActionType => ActionType.Move;


        /// <summary>
        /// The location the move departs from (the previous stop).
        /// </summary>
        public InlineAssociationType<Location> From { get; set; }


        /// <summary>
        /// The location the move arrives at (the next stop).
        /// </summary>
        public InlineAssociationType<Location> To { get; set; }


        /// <summary>
        /// The route driven from <see cref="From"/> to <see cref="To"/>, carrying the geometry as a
        /// <see cref="LatLonArrayGeoReference"/> on the <see cref="v5.Route"/> entity.
        /// </summary>
        public InlineAssociationType<Route> Route { get; set; }
    }
}
