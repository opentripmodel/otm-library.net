namespace OpenTripModel.v5
{
    public class InlineAssociationType<T>
    {
        /// <summary>
        /// Type of association
        /// </summary>
        public AssociationType AssociationType => AssociationType.Inline;


        /// <summary>
        /// Associated entity
        /// </summary>
        public T Entity { get; set; }


        /// <summary>
        /// A free text description of the relationship to the associated entity.
        /// </summary>
        public System.String Description { get; set; }
    }
}
