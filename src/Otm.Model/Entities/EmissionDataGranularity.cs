namespace OpenTripModel.v5
{
    /// <summary>
    /// The level of detail of the data underlying a CO₂(e) emission calculation.
    /// </summary>
    public enum EmissionDataGranularity
    {
        /// <summary>Data per day.</summary>
        Day,

        /// <summary>Data per week.</summary>
        Week,

        /// <summary>Data per month.</summary>
        Month,

        /// <summary>Data per quarter.</summary>
        Quarter,

        /// <summary>Data per year.</summary>
        Year
    }
}
