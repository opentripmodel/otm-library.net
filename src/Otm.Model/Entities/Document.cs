namespace OpenTripModel.v5
{
    public class Document
        : OtmEntity
    {
        /// <summary>
        /// Any content
        /// </summary>
        public Content Content { get; set; }

        /// <summary>
        /// The type of the document, such as a photo, text document, PDF etc.
        /// </summary>
        public System.String DocumentType { get; set; }


        /// <summary>
        /// The name of the file.
        /// </summary>
        public System.String Filename { get; set; }


        /// <summary>
        /// The official MIME type of the file. See Wikepedia for more information.
        /// </summary>
        public System.String MimeType { get; set; }



        /// <summary>
        /// The description of the file, for example what purpose it serves.
        /// </summary>
        public System.String Description { get; set; }


        /// <summary>
        /// The actor who owns the document. 
        /// If not provided, the creator will be assumed to be the owner.
        /// </summary>
        public Actor Creator { get; set; }


        /// <summary>
        /// The actor who owns the document. 
        /// If not provided, the creator will be assumed to be the owner.
        /// </summary>
        public Actor Owner { get; set; }
    }
}
