using System;

namespace OpenTripModel.v5
{
    /// <summary>
    /// Reports the energy (fuel, electricity, etc.) consumed by a vehicle, towed unit, auxiliary
    /// equipment or hub location over a given period. Used as context event in the OTM
    /// Carbon Footprint profile (CO₂ Calculation Input) to supply the energy data needed to
    /// calculate CO₂ emissions, as defined by the DALTI "Uniforme kernset CO₂e".
    /// </summary>
    public class EnergyConsumptionEvent
        : Event
    {
        public override EventType EventType => EventType.EnergyConsumptionEvent;


        /// <summary>
        /// The type of energy that was consumed, e.g. diesel, electricity or hydrogen.
        /// Use <see cref="Fuel.Other"/> in combination with <see cref="OtherEnergySourceDescription"/>
        /// for energy sources not covered by the enumeration.
        /// </summary>
        public Fuel? EnergySource { get; set; }


        /// <summary>
        /// Description of the energy source, only to be used when <see cref="EnergySource"/> is set to other.
        /// </summary>
        public System.String OtherEnergySourceDescription { get; set; }


        /// <summary>
        /// The amount of energy consumed in the given period, e.g. liters of diesel or kWh of electricity.
        /// </summary>
        public UnitWithValue Quantity { get; set; }


        /// <summary>
        /// The standard ("normering") the consumption figures adhere to and against which emission
        /// factors should be applied, e.g. "GLEC" or "CO2emissiefactoren.nl".
        /// </summary>
        public System.String EmissionFactorStandard { get; set; }


        /// <summary>
        /// Start of the period over which the energy consumption is reported.
        /// </summary>
        public Nullable<DateTime> PeriodStart { get; set; }


        /// <summary>
        /// End of the period over which the energy consumption is reported.
        /// </summary>
        public Nullable<DateTime> PeriodEnd { get; set; }


        /// <summary>
        /// The vehicle (or towed unit / auxiliary equipment) the consumption applies to. Optional when
        /// the event is provided as context of a trip with a single vehicle.
        /// </summary>
        public InlineAssociationType<Vehicle> Vehicle { get; set; }


        /// <summary>
        /// The (hub) location the consumption applies to, used to report hub energy data (HOC).
        /// Should be omitted for vehicle-related consumption.
        /// </summary>
        public InlineAssociationType<Location> Location { get; set; }
    }
}
