using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Trip
        : OtmEntity
    {
        /// <summary>
        /// Whether this trip is requested, accepted, modified or cancelled.
        /// </summary>
        public TripStatus Status { get; set; }


        /// <summary>
        /// Method of transport used for the carriage of goods on this trip, can either be using a ship
        /// (maritime or inland waterway), a truck/car/van/bike/etc. (road), using the train (rail), or 
        /// using a plane (air). These values are based on the recommendation of UNECE.
        /// </summary>
        public TransportMode? TransportMode { get; set; }


        public EntityType? EntityType { get; set; }


        /// <summary>
        /// The Vehicle that is driving this trip.
        /// </summary>
        public InlineAssociationType<Vehicle> Vehicle { get; set; }


        /// <summary>
        /// The actors associated with this trip, for instance the client or the executing party
        /// </summary>
        public List<InlineAssociationType<Actor>> Actors { get; set; }


        /// <summary>
        /// All actions that are/were happening on this trip, such as stopping at certain locations and loading and 
        /// unloading of consignments.
        /// </summary>
        public List<InlineAssociationType<StopAction>> Actions { get; set; }


        /// <summary>
        /// Constraints this trip has to abide to, such as the start and end date times in which it has to be completed.
        /// </summary>
        public List<InlineAssociationType<Constraint>> Constraints { get; set; }
    }
}
