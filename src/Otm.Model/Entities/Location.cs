using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Location
        : OtmEntity
    {
        /// <summary>
        /// The type of location
        /// </summary>
        public LocationType Type { get; set; }


        /// <summary>
        /// Describes a geographic reference, this is the primary way to link a 
        /// Location entity to a physical, geographic location.
        /// </summary>
        public GeoReference GeoReference { get; set; }


        /// <summary>
        /// the United Nations Code for Trade and Transport Locations, is a geographic coding scheme 
        /// developed and maintained by United Nations Economic Commission for Europe (UNECE) to uniquely 
        /// identify locations.
        /// </summary>
        public System.String UnCode { get; set; }


        /// <summary>
        /// The Global Location Number (GLN) is part of the GS1 systems of standards to uniquely identify a location. 
        /// </summary>
        public System.String Gln { get; set; }


        /// <summary>
        /// Address information that is used as an administrative reference. For example: when the actual load 
        /// location is different from the officially registered location, this holds the latter
        /// </summary>
        public AdministrativeReference AdministrativeReference { get; set; }


        /// <summary>
        /// Contact details for this Location
        /// </summary>
        public List<ContactDetail> ContactDetails { get; set; }


        /// <summary>
        /// Remark about the location. Please don't misuse this field for external 
        /// references, use the externalAttributes field instead.
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
    }
}