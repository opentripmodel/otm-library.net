namespace OpenTripModel.v5
{
    /// <summary>
    /// The share of one data type (primary, modelled or default) within the data used for a
    /// CO₂(e) emission calculation.
    /// </summary>
    public class EmissionDataShare
    {
        /// <summary>
        /// The type of data this share applies to.
        /// </summary>
        public EmissionDataType DataType { get; set; }


        /// <summary>
        /// The fraction (0..1) of the underlying data that is of this data type.
        /// </summary>
        public System.Nullable<System.Double> Share { get; set; }
    }
}
