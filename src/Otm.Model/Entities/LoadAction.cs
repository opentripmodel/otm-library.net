namespace OpenTripModel.v5
{
    /// <summary>
    /// An action that represents loading one or more consignments onto a vehicle.
    /// Typically nested under a <see cref="StopAction"/>'s <see cref="Action.Actions"/> collection.
    /// </summary>
    public class LoadAction
        : Action
    {
        /// <summary>
        /// Initializes a new <see cref="LoadAction"/> defaulted to <see cref="ActionType.Load"/>.
        /// </summary>
        public LoadAction()
        {
            ActionType = ActionType.Load;
        }
    }
}
