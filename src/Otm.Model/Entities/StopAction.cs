namespace OpenTripModel.v5
{
    public class StopAction
        : Action
    {
        /// <summary>
        /// Initializes a new <see cref="StopAction"/> defaulted to <see cref="ActionType.Stop"/>.
        /// The <see cref="Action.ActionType"/> remains settable so callers can override it where the
        /// profile permits (e.g. <see cref="ActionType.Move"/> in the Carbon Footprint profile).
        /// </summary>
        public StopAction()
        {
            ActionType = ActionType.Stop;
        }


        /// <summary>
        /// The sequence NR of this stop within the trip it is taking place.
        /// </summary>
        public int? SequenceNr { get; set; }
    }
}