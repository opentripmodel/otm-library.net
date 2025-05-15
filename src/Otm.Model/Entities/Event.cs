using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public abstract class Event
        : OtmEntity

    {
        public abstract EventType EventType { get; }



        /// <summary>
        /// A lifecycle models when the data in the action is taking place. You can provide the same action in multiple 
        /// lifecycles to model how it changes over time. For example the planned and realized time of an action taking 
        /// place can differ because of unforseen circumstances (such as traffic jams).
        /// </summary>
        public Lifecycle Lifecycle { get; set; }



        /// <summary>
        /// The actors associated with this transport order, for instance the consignor, consignee.
        /// </summary>
        public IEnumerable<InlineAssociationActorType> Actors { get; set; }
    }
}
