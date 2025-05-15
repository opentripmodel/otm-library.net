namespace OpenTripModel.v5
{
    public class GenericConstraint 
        : ConstraintValueBase
    {
        public override ConstraintTypeEnum Type => ConstraintTypeEnum.GenericConstraint;


        /// <summary>
        /// A free text description of the constraint
        /// </summary>
        public System.String Description { get; set; }
    }


    public class AndConstraint
        : GenericConstraint
    {
        public override ConstraintTypeEnum Type => ConstraintTypeEnum.AndConstraint;


        /// <summary>
        /// All constraints in the array are combined using a boolean AND relation. 
        /// This means that the resulting constraint will only apply if all constraints 
        /// in the array would apply.
        /// </summary>
        public ConstraintValueBase[] And { get; set; }
    }


    public class OrConstraint
    : GenericConstraint
    {
        public override ConstraintTypeEnum Type => ConstraintTypeEnum.OrConstraint;


        /// <summary>
        /// All constraints in the array are combined using a boolean OR relation. This means 
        /// that the resulting constraint will apply if any of the constraints in the array 
        /// would apply.
        /// </summary>
        public ConstraintValueBase[] Or { get; set; }
    }


    public class TimeWindowConstraint
        : GenericConstraint
    {
        public override ConstraintTypeEnum Type => ConstraintTypeEnum.TimeWindowConstraint;


        /// <summary>
        /// This constraint applies from the given start date/time.
        /// </summary>
        public System.DateTime StartTime { get; set; }



        /// <summary>
        /// This constraint applies until the given start date/time.
        /// </summary>
        public System.DateTime EndTime { get; set; }
    }

    public enum ValueBoundConstraintValueType { Sensor, Speed, Weight, Temperature, Height };

    public enum ValueBoundConstraintType { Maximum, Minimum, Range };

    public class ValueBoundConstraint
    : GenericConstraint
    {
        public override ConstraintTypeEnum Type => ConstraintTypeEnum.ValueBoundConstraint;


        /// <summary>
        /// The type of value that has bounds. Currently supported are Sensor (update frequency), 
        /// Speed (max and min speeds), Weight (max and min weights of the vehicle or goods) and 
        /// Temperature (max and min temperature of the goods).
        /// </summary>
        public ValueBoundConstraintValueType ValueType { get; set; }


        /// <summary>
        /// The type of value that has bounds. Currently supported are Sensor (update frequency), 
        /// Speed (max and min speeds), Weight (max and min weights of the vehicle or goods) and 
        /// Temperature (max and min temperature of the goods).
        /// </summary>
        public ValueBoundConstraintType ConstraintType { get; set; }

        public UnitWithValue Maximum { get; set; }

        public UnitWithValue Minimum { get; set; }

    }


}
