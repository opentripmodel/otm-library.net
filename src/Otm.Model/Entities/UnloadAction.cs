namespace OpenTripModel.v5
{
    /// <summary>
    /// An action that represents unloading one or more consignments from a vehicle.
    /// Typically nested under a <see cref="StopAction"/>'s <see cref="Action.Actions"/> collection.
    /// </summary>
    public class UnloadAction
        : Action
    {
        /// <summary>
        /// Initializes a new <see cref="UnloadAction"/> defaulted to <see cref="ActionType.Unload"/>.
        /// </summary>
        public UnloadAction()
        {
            ActionType = ActionType.Unload;
        }
    }
}
