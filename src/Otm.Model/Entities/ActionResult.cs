namespace OpenTripModel.v5
{
    public class ActionResult
    {
        /// <summary>
        /// The status of the result, either succeeded, failed, partially succeeded or cancelled.
        /// </summary>
        public ActionResultStatus Status { get; set; }

        /// <summary>
        /// The remark of the result, usually only interesting in case the result was failed or 
        /// partially succeedded. Note that differs from the remark on an action, which is a remark 
        /// that is relevant before the execution of the action. Whereas this remark is relevant for 
        /// the result after execution.
        /// </summary>
        public System.String Remark { get; set; }


        /// <summary>
        /// The reason why the action (partially) failed. Currently the only supported reasons are 
        /// 'damage' and receiverAbsent. E.g. as an example of the former: the handOver failed because 
        /// the goods were too damaged and were rejected. An example of the latter is when the goods 
        /// were planned to be delivered but nobody was there to receive the goods and therefore had 
        /// to be taken back.
        /// </summary>
        public System.String Reason { get;set; }
    }
}
