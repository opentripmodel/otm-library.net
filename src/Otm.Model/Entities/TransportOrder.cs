using System.Collections.Generic;
namespace OpenTripModel.v5
{
    public class TransportOrder
        : OtmEntity
    {
        /// <summary>
        /// A free text description of this transport order
        /// </summary>
        public System.String Description { get; set; }


        /// <summary>
        /// All consignments belonging to this transport order.
        /// </summary>
        public List<InlineAssociationType<Consignment>> Consignments { get; set; }


        /// <summary>
        /// The actors associated with this transport order, for instance the consignor, consignee.
        /// </summary>
        public List<InlineAssociationActorType> Actors { get; set; }


        /// <summary>
        /// Constraints this transport order has to abide to, such as the expected delivery time windows.
        /// </summary>
        public List<InlineAssociationType<Constraint>> Constraints { get; set; }
    }

    // test dotnet affected: model
}
