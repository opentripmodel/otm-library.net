using System;
using System.Collections.Generic;


namespace OpenTripModel.v5
{
    public class Vehicle
        : OtmEntity
    {
        /// <summary>
        /// The type of vehicle
        /// </summary>
        public System.String VehicleType { get; set; }


        
        /// <summary>
        /// The type of fuel the vehicle runs on. For vehicle without an engine of their own, such as a 
        /// trailer, you may choose not-applicable. For trailers with cooling capabilities, choose the 
        /// fuel type of the cooling engine.
        /// </summary>
        public Fuel? Fuel { get; set; }

        
        
        /// <summary>
        /// Type of fuel, only to be used when the fuel field is set to other
        /// </summary>
        public System.String OtherFuelType { get; set; }



        /// <summary>
        /// Type of fuel, only to be used when the fuel field is set to other
        /// </summary>
        public UnitWithValue AverageFuelConsumption { get; set; }



        /// <summary>
        /// European emission standards are vehicle emission standards for exhaust emissions of new vehicles sold in 
        /// the European Union and EEA member states. The standards are defined in a series of European Union directives 
        /// staging the progressive introduction of increasingly stringent standards.
        /// </summary>
        public EmissionStandard? EmissionStandard { get; set; }



        /// <summary>
        /// Maximum number of links to other Vehicles. 
        /// Typical values are 0, 1 or 2.
        /// </summary>
        public Nullable<System.Int32> MaxLinks { get; set; }



        /// <summary>
        /// The load capacities of the Vehicle. This can be an array of values, for several reasons:
        ///   - The Vehicle might be split up in multiple compartments.
        ///   - You might want to express the load capacities in different quantities. 
        ///     E.g. in square meters or litres as well as in number of pallets.
        /// </summary>
        public List<UnitWithValue> LoadCapacities { get; set; }



        /// <summary>
        /// The length of the Vehicle
        /// </summary>
        public UnitWithValue Length { get; set; }



        /// <summary>
        /// The height of the Vehicle
        /// </summary>
        public UnitWithValue Height { get; set; }



        /// <summary>
        /// The width of the Vehicle
        /// </summary>
        public UnitWithValue Width { get; set; }



        /// <summary>
        /// The license plate of the vehicle.
        /// </summary>
        public System.String LicensePlate { get; set; }



        /// <summary>
        /// The weight of the Vehicle when empty.
        /// </summary>
        public UnitWithValue EmptyWeight { get; set; }



        /// <summary>
        /// There are multiple roles in which actors can be associated with a vehicle, such as the owner 
        /// or the driver of the vehicle.
        /// </summary>
        public List<InlineAssociationType<Actor>> Actors { get; set; }



        /// <summary>
        /// Vehicles might have some sensors that are permanently attached, these can be described using the sensors field. 
        /// If one works with detachable sensors the recommend approach is to use associationCreated and associationRemoved 
        /// events instead.
        /// </summary>
        public List<InlineAssociationType<Sensor>> Sensors { get; set; }



        /// <summary>
        /// The actions that are involved for the vehicle (for a particular time window)
        /// </summary>
        public List<InlineAssociationType<Action>> Actions { get; set; }
    }
}
