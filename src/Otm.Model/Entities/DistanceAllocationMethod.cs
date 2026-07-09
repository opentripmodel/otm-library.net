namespace OpenTripModel.v5
{
    /// <summary>
    /// The method used to allocate CO₂(e) emissions over transport distance.
    /// </summary>
    public enum DistanceAllocationMethod
    {
        /// <summary>
        /// Great circle distance: the shortest distance between two points over the earth's surface.
        /// </summary>
        Gcd,

        /// <summary>
        /// Shortest feasible distance: the shortest distance that can actually be travelled by the
        /// mode of transport.
        /// </summary>
        Sfd
    }
}
