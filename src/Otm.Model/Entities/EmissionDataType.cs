namespace OpenTripModel.v5
{
    /// <summary>
    /// The origin of the data used in a CO₂(e) emission calculation.
    /// </summary>
    public enum EmissionDataType
    {
        /// <summary>
        /// Measured data from the actual transport operation, e.g. real fuel consumption.
        /// </summary>
        Primary,

        /// <summary>
        /// Data derived from a model of the transport operation.
        /// </summary>
        Modelled,

        /// <summary>
        /// Default (industry average) values.
        /// </summary>
        Default
    }
}
