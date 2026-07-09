using System;

namespace OpenTripModel.v5
{
    /// <summary>
    /// The EmissionEvent contains information about how much emission has been produced during a
    /// move, on a trip or per consignment, as defined by the OTM 5.7 specification. In the OTM
    /// Carbon Footprint profile (CO₂ Calculation Result) this event carries the calculated CO₂(e)
    /// emissions returned by a calculation platform, including the calculation transparency
    /// required by the DALTI "Uniforme kernset CO₂e" result dataset.
    /// </summary>
    public class EmissionEvent
        : Event
    {
        /// <inheritdoc />
        public override EventType EventType => EventType.EmissionEvent;


        /// <summary>
        /// The amount of emitted pollutants in this event, expressed in CO₂ equivalents,
        /// e.g. { value: 123.4, unit: "kgCO2e" }. Required by the OTM specification.
        /// </summary>
        public UnitWithValue Amount { get; set; }


        /// <summary>
        /// The method used to determine the amount of emissions, as free text. Especially useful
        /// in the projected/planned lifecycle where an estimation is made. For the structured
        /// calculation transparency of the Carbon Footprint profile use
        /// <see cref="CalculationMethod"/>.
        /// </summary>
        public System.String Method { get; set; }


        /// <summary>
        /// The emission intensity over the reported period, e.g. { value: 62, unit: "gCO2e/tkm" },
        /// based on the carbon performance indicator (CPI) of the transport activity. Carbon
        /// Footprint profile extension.
        /// </summary>
        public UnitWithValue EmissionIntensity { get; set; }


        /// <summary>
        /// Start of the period the emission result applies to. Carbon Footprint profile extension.
        /// </summary>
        public Nullable<DateTime> PeriodStart { get; set; }


        /// <summary>
        /// End of the period the emission result applies to. Carbon Footprint profile extension.
        /// </summary>
        public Nullable<DateTime> PeriodEnd { get; set; }


        /// <summary>
        /// Describes how the emission was calculated: the framework, ISO reference, allocation
        /// method, data quality and granularity. Required by the Carbon Footprint profile for
        /// transparency and comparability of emission figures between platforms.
        /// </summary>
        public EmissionCalculationMethod CalculationMethod { get; set; }


        /// <summary>
        /// The vehicle that is relevant for this event, used for vehicle-level emission reporting.
        /// </summary>
        public InlineAssociationType<Vehicle> Vehicle { get; set; }


        /// <summary>
        /// The consignment that is relevant for this event.
        /// </summary>
        public InlineAssociationType<Consignment> Consignment { get; set; }


        /// <summary>
        /// The (hub) location the emission result applies to, used for hub-level (HOC) emission
        /// reporting. Carbon Footprint profile extension; should be omitted for trip- or
        /// vehicle-level results.
        /// </summary>
        public InlineAssociationType<Location> Location { get; set; }
    }
}
