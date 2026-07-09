namespace OpenTripModel.v5
{
    public enum EventType
    {
        UpdateEvent,
        LocationUpdateEvent,

        /// <summary>Fuel/energy consumed during a move, on a trip or per consignment.</summary>
        FuelConsumedEvent,

        /// <summary>Emission produced during a move, on a trip or per consignment.</summary>
        EmissionEvent
    }
}
