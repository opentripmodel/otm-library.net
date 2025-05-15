using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Route
        : OtmEntity
    {
        /// <summary>
        /// Geographic representation of this route.
        /// </summary>
        public LatLonArrayGeoReference GeoReferences { get; set; }


        /// <summary>
        /// Actors associated with this route, for instance the Company that requires this route as a Last-Mile guidance.
        /// </summary>
        public List<InlineAssociationActorType> Actors { get; set; }


        /// <summary>
        /// Actors associated with this route, for instance the Company that requires this route as a Last-Mile guidance.
        /// </summary>
        public List<InlineAssociationType<StopAction>> Actions { get; set; }


        /// <summary>
        /// Constraints this transport order has to abide to, such as the expected delivery time windows.
        /// </summary>
        public InlineAssociationType<Constraint> Constraint { get; set; }
    }
}
