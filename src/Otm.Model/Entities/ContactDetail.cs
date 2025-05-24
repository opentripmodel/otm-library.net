namespace OpenTripModel.v5
{
    public class ContactDetail
    {
        public ContactDetailType Type { get; set; }

        /// <summary>
        /// A phone number, note that this means the land line phone number. 
        /// Mobile phone is a separate type of contact detail
        /// </summary>
        public System.String Value { get; set; }


        /// <summary>
        /// Remark for this contain detail.
        /// </summary>
        public System.String Remark { get; set; }



        /// <summary>
        /// Language required for contacting. One must use the ISO 639-3 codes.
        /// </summary>
        public System.String Language { get; set; }
    }
}