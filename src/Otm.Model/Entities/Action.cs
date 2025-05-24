using System;
using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Action
        : OtmEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual ActionType ActionType { get; set; }


        /// <summary>
        /// A lifecycle models when the data in the action is taking place. You can provide the same action in multiple 
        /// lifecycles to model how it changes over time. For example the planned and realized time of an action taking 
        /// place can differ because of unforseen circumstances (such as traffic jams).
        /// </summary>
        public Lifecycle Lifecycle { get; set; }


        /// <summary>
        /// Free text field for adding an on remark on this action.
        /// </summary>
        public System.String Remark { get; set; }


        /// <summary>
        /// The stop that is associated with this action.
        /// </summary>
        public InlineAssociationType<StopAction> Stop { get; set; }


        /// <summary>
        /// The consignment that is the subject of this action.
        /// </summary>
        public InlineAssociationType<Consignment> Consignment { get; set; }



        /// <summary>
        /// The location at which this action is taking place.
        /// </summary>
        public InlineAssociationType<Location> Location { get; set; }


        /// <summary>
        /// The time at which the actions starts
        /// </summary>
        public System.Nullable<DateTime> StartTime { get; set; }


        /// <summary>
        /// The time at which the action is completed
        /// </summary>
        public System.Nullable<DateTime> EndTime { get; set; }


        /// <summary>
        /// Associations actions
        /// </summary>
        public List<InlineAssociationType<Action>> Actions { get; set; }


        /// <summary>
        /// The constraints this action abides to, such as start and end time windows.
        /// </summary>
        public List<InlineAssociationType<Constraint>> Constraint { get; set; }


        /// <summary>
        /// The result of the action, can only be present in the actual or realized 
        /// lifecycles. The result has a required status and optional additional info, 
        /// like a remark and reason (in the case of failure).
        /// </summary>
        public List<ActionResult> Result { get; set; }


        /// <summary>
        /// The sequence number of this action within the entity it is taking place. 
        /// Can be used to indicate order when no times are present
        /// </summary>
        public Nullable<System.Int32> SequenceNr { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public TimeFormat TimeFormat { get; set; }



        /// <summary>
        /// The recurrence of the date time, should only be set when timeFormat is set 
        /// to recurringDateTime. The recurrence follows the Recurrence Rule specification
        /// </summary>
        public System.String Recurrence { get; set; }



        /// <summary>
        /// The duration of this action/event, should only be set when timeFormat is set
        /// to duration. The duration follows the ISO 8601 specification
        /// </summary>
        public System.String Duration { get; set; }
    }
}