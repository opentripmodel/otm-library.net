namespace OpenTripModel.v5
{
    /// <summary>
    /// Quality classification of a CO₂(e) emission figure, based on the origin and level of
    /// detail of the underlying data.
    /// </summary>
    public enum EmissionDataClassification
    {
        /// <summary>Lowest data quality, largely based on default values.</summary>
        Bronze,

        /// <summary>Mixed data quality with a substantial modelled or default share.</summary>
        Silver,

        /// <summary>High data quality, largely based on primary data.</summary>
        Gold,

        /// <summary>Highest data quality, fully based on verified primary data.</summary>
        GoldPlus
    }
}
