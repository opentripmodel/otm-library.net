using System;

namespace OpenTripModel.v5
{
    /// <summary>
    /// The FuelConsumedEvent contains information about how much fuel (or other energy, such as
    /// electricity) was consumed during a move, on a trip or per consignment, as defined by the
    /// OTM 5.7 specification. In the OTM Carbon Footprint profile (CO₂ Calculation Input) this
    /// event supplies the energy data needed to calculate CO₂ emissions, as defined by the DALTI
    /// "Uniforme kernset CO₂e": the consumption of a vehicle, towed unit, auxiliary equipment or
    /// hub location over a given period.
    /// </summary>
    public class FuelConsumedEvent
        : Event
    {
        /// <inheritdoc />
        public override EventType EventType => EventType.FuelConsumedEvent;


        /// <summary>
        /// The type of fuel (energy) consumed in this event, e.g. diesel, electricity or hydrogen.
        /// Use <see cref="v5.Fuel.Other"/> in combination with <see cref="OtherFuelType"/> for
        /// energy sources not covered by the enumeration.
        /// </summary>
        public Fuel? Fuel { get; set; }


        /// <summary>
        /// Type of fuel, only to be used when the fuel field is set to other.
        /// </summary>
        public System.String OtherFuelType { get; set; }


        /// <summary>
        /// The amount of fuel (energy) consumed in this event, e.g. liters of diesel or kWh of
        /// electricity. Required by the OTM specification.
        /// </summary>
        public UnitWithValue Amount { get; set; }


        /// <summary>
        /// The standard ("normering") the consumption figures adhere to and against which emission
        /// factors should be applied, e.g. "GLEC" or "CO2emissiefactoren.nl". Carbon Footprint
        /// profile extension.
        /// </summary>
        public System.String EmissionFactorStandard { get; set; }


        /// <summary>
        /// Start of the period over which the consumption is reported. Carbon Footprint profile
        /// extension.
        /// </summary>
        public Nullable<DateTime> PeriodStart { get; set; }


        /// <summary>
        /// End of the period over which the consumption is reported. Carbon Footprint profile
        /// extension.
        /// </summary>
        public Nullable<DateTime> PeriodEnd { get; set; }


        /// <summary>
        /// The vehicle (or towed unit / auxiliary equipment) that is relevant for this event.
        /// Optional when the event is provided as context of a trip with a single vehicle.
        /// </summary>
        public InlineAssociationType<Vehicle> Vehicle { get; set; }


        /// <summary>
        /// The consignment that is relevant for this event.
        /// </summary>
        public InlineAssociationType<Consignment> Consignment { get; set; }


        /// <summary>
        /// The (hub) location the consumption applies to, used to report hub energy data (HOC).
        /// Carbon Footprint profile extension; should be omitted for vehicle-related consumption.
        /// </summary>
        public InlineAssociationType<Location> Location { get; set; }
    }
}
