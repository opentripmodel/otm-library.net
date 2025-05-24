using System;
using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Consignment
        : OtmEntity
    {
        /// <summary>
        /// General description of consignment in Free text. e.g 20 europallets fruit.
        /// </summary>
        public System.String Description { get; set; }



        /// <summary>
        /// Whether this consignment is requested, accepted, modified or cancelled.
        /// </summary>
        public ConsignmentStatus Status { get; set; }


        /// <summary>
        /// Free text to describe type of consignment e.g. FTL, Full Truck Load, LTL Less 
        /// than Truck Load, bulk, reverse logistics orders, pick up order, delivery order.
        /// </summary>
        public System.String Type { get; set; }


        /// <summary>
        /// The various goods that need to be transported, together they are part of this consignment.
        /// </summary>
        public List<InlineAssociationType<GoodItem>> Goods { get; set; }


        /// <summary>
        /// Documents that are relevant for this consignment. 
        /// Such as an official agreement between consignee and consignor.
        /// </summary>
        public List<InlineAssociationType<Document>> Documents { get; set; }



        /// <summary>
        /// Remark concerning the complete consignment, to be printed on the transport document.
        /// </summary>
        public System.String Remark { get; set; }


        /// <summary>
        /// The actors associated with this consignment, for instance the shipper and carrier.
        /// </summary>
        public List<InlineAssociationType<Actor>> Actors { get; set; }


        /// <summary>
        /// General description of actions related to the consignment f.e. loading, unloading, 
        /// hand over, drop of.
        /// </summary>
        public List<InlineAssociationType<Action>> Actions { get; set; }


        /// <summary>
        /// Constraints this consignment has to abide to, such special equipment (tail lift, 
        /// truck mounted forklift), special vehicle, special instructions related to consignor 
        /// and consignee.
        /// </summary>
        public InlineAssociationType<Constraint> Constraint { get; set; }


        /// <summary>
        /// The transport order this consignment belongs to.
        /// </summary>
        public InlineAssociationType<TransportOrder> TransportOrder { get; set; }



        /// <summary>
        /// Consignments that have replaced the current consignment. Because of various reasons a 
        /// consignment can be cancelled and replaced by one or more other consignments. An example 
        /// is that the consignment is too large to be transported as a single 'transportable unit'. 
        /// You can use the relation field in the association to indicate the type of relationship.
        /// </summary>
        public List<InlineAssociationType<Consignment>> RelatedConsignments { get; set; }
    }
}