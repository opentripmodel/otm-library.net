using System.Text.Json.Serialization;

namespace OpenTripModel.v5
{
    public class InlineAssociationType<T>
    {
        /// <summary>
        /// Type of association. Fixed to <see cref="v5.AssociationType.Inline"/>. The explicit
        /// <see cref="JsonIgnoreAttribute"/> override forces serialization even under a
        /// <see cref="JsonIgnoreCondition.WhenWritingDefault"/> policy — the value coincides with
        /// the enum's default (<c>Inline = 0</c>) and would otherwise be suppressed, violating any
        /// profile schema that requires the property to be present on every inline association.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
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
