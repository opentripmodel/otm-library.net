namespace OpenTripModel.v5
{
    public class StopAction
        : Action
    {
        public override ActionType ActionType => ActionType.Stop;


        /// <summary>
        /// The sequence NR of this stop within the trip it is taking place.
        /// </summary>
        public System.Int64 SequenceNr { get; set; }
    }
}