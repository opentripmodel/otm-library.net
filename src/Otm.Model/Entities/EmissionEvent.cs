using System;

namespace OpenTripModel.v5
{
    /// <summary>
    /// Carries a calculated CO₂(e) emission result, as returned by a CO₂ calculation platform.
    /// Used as context event in the OTM Carbon Footprint profile (CO₂ Calculation Result) and based
    /// on the DALTI "Uniforme kernset CO₂e" result dataset. Depending on the associations set, the
    /// result applies to the surrounding trip, a specific vehicle or a hub location.
    /// </summary>
    public class EmissionEvent
        : Event
    {
        public override EventType EventType => EventType.EmissionEvent;


        /// <summary>
        /// The total emission over the reported period, expressed in CO₂ equivalents,
        /// e.g. { value: 123.4, unit: "kgCO2e" }.
        /// </summary>
        public UnitWithValue TotalEmission { get; set; }


        /// <summary>
        /// The emission intensity over the reported period, e.g. { value: 62, unit: "gCO2e/tkm" },
        /// based on the carbon performance indicator (CPI) of the transport activity.
        /// </summary>
        public UnitWithValue EmissionIntensity { get; set; }


        /// <summary>
        /// Start of the period the emission result applies to.
        /// </summary>
        public Nullable<DateTime> PeriodStart { get; set; }


        /// <summary>
        /// End of the period the emission result applies to.
        /// </summary>
        public Nullable<DateTime> PeriodEnd { get; set; }


        /// <summary>
        /// Describes how the emission was calculated: the framework, ISO reference, allocation
        /// method, data quality and granularity. Required for transparency and comparability
        /// of emission figures between platforms.
        /// </summary>
        public EmissionCalculationMethod CalculationMethod { get; set; }


        /// <summary>
        /// The vehicle the emission result applies to, used for vehicle-level emission reporting.
        /// </summary>
        public InlineAssociationType<Vehicle> Vehicle { get; set; }


        /// <summary>
        /// The (hub) location the emission result applies to, used for hub-level (HOC) emission
        /// reporting. Should be omitted for trip- or vehicle-level results.
        /// </summary>
        public InlineAssociationType<Location> Location { get; set; }
    }
}
