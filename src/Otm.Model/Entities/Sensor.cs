using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Sensor
        : OtmEntity
    {
        /// <summary>
        /// Sometimes more than one sensor can be associated with a single entity. This is the case e.g. in cooled 
        /// trailers that are divided into compartments with different temperatures, where each compartment has its
        /// own sensor. The placement member can be used to identify where a sensor is placed. Parties using OpenTripModel 
        /// to exchange sensor data may wish to agree on a standardized naming, but this is too specific to describe in 
        /// the standard.
        /// </summary>
        public System.String Placement {  get; set; }



        /// <summary>
        /// The type of sensor.
        /// </summary>
        public System.String SensorType {  get; set; }



        /// <summary>
        /// The actors associated with this sensor, for instance the owners, the producers or the users of this sensor.
        /// </summary>
        public List<InlineAssociationType<Actor>> Actors { get; private set; } = new List<InlineAssociationType<Actor>>();



        /// <summary>
        /// In the context of a Sensor, only sensorValueConstraints really make sense. You can use such a constraint to 
        /// model a sensor of which the measured value must be within certain bounds at all times.
        /// </summary>
        public InlineAssociationType<Constraint> Constraint { get; set; }
    }
}
