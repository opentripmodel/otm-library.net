using System.Collections.Generic;

namespace OpenTripModel.v5
{
    /// <summary>
    /// Describes how a CO₂(e) emission figure was calculated. Corresponds to the generic
    /// ("Generiek") elements of the DALTI "Uniforme kernset CO₂e" result dataset.
    /// </summary>
    public class EmissionCalculationMethod
    {
        /// <summary>
        /// The calculation framework and version that was used, e.g. "GLEC 3.1" or
        /// "CO2emissiefactoren.nl".
        /// </summary>
        public System.String Framework { get; set; }


        /// <summary>
        /// Reference to the ISO standard version the calculation complies with, e.g. "ISO 14083:2023".
        /// </summary>
        public System.String IsoReference { get; set; }


        /// <summary>
        /// The method used to allocate emissions over distance: great circle distance (GCD) or
        /// shortest feasible distance (SFD).
        /// </summary>
        public DistanceAllocationMethod? DistanceAllocationMethod { get; set; }


        /// <summary>
        /// The level of detail of the underlying data: day, week, month, quarter or year.
        /// </summary>
        public EmissionDataGranularity? Granularity { get; set; }


        /// <summary>
        /// Quality classification of the emission figure: bronze, silver, gold or gold+.
        /// </summary>
        public EmissionDataClassification? DataClassification { get; set; }


        /// <summary>
        /// The distribution of the underlying data over the data types primary, modelled and
        /// default, expressed as fractions that together add up to 1.
        /// </summary>
        public List<EmissionDataShare> DataTypeDistribution { get; set; }
    }
}
