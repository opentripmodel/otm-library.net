using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Actor
        : OtmEntity
    {
        /// <summary>
        /// Entity discriminator. Profiles that require <c>entityType=actor</c> on every actor
        /// (for example Carbon Footprint input) expect this field to be populated with
        /// <see cref="v5.EntityType.Actor"/>. Other profiles (e.g. CBS) disallow the field —
        /// hence it is nullable with no default, leaving the choice to the caller.
        /// </summary>
        public EntityType? EntityType { get; set; }


        /// <summary>
        /// Contact details for this Actor.
        /// </summary>
        public List<ContactDetail> ContactDetails { get; set; }



        /// <summary>
        /// Locations for this Actor.
        /// </summary>
        public List<InlineAssociationType<Location>> Locations { get; set; }
    }
}