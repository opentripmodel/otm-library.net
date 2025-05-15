using System;

namespace OpenTripModel.v5
{
    public class UnitWithValue
    {
        public static UnitWithValue Create( Nullable<System.Double> value, System.String unit )
        {
            return value == null
                ? null
                : new UnitWithValue() { Value = value, Unit = unit };
        }

        // Implicit conversion from double to UnitWithValue
        public static implicit operator UnitWithValue(double value)
        {
            return new UnitWithValue { Value = value, Unit = null };
        }



        /// <summary>
        /// Measurement unit of this value. It's best to adhere to the International System of Units 
        /// as much as possible.
        /// </summary>
        public System.String Unit { get; set; }


        /// <summary>
        /// Value in the given unit.
        /// </summary>
        public Nullable<System.Double> Value { get; set; }
    }
}
