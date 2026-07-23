namespace OpenTripModel.v5
{
    /// <summary>
    /// An action that represents the movement of a vehicle between two stops.
    /// Typically holds the <see cref="Action.Distance"/> travelled and is nested under a
    /// <see cref="StopAction"/> (or sits alongside it) in the Carbon Footprint profile.
    /// </summary>
    public class MoveAction
        : Action
    {
        /// <summary>
        /// Initializes a new <see cref="MoveAction"/> defaulted to <see cref="ActionType.Move"/>.
        /// </summary>
        public MoveAction()
        {
            ActionType = ActionType.Move;
        }
    }
}
