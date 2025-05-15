using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Actor
        : OtmEntity
    {
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