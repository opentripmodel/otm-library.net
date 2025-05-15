namespace OpenTripModel.v5
{
    public class AddressGeoReference
        : GeoReference
    {
        public override GeoReferenceType Type => GeoReferenceType.AddressGeoReference;

        /// <summary>
        /// Name of the address.
        /// </summary>
        public System.String Name { get; set; }


        /// <summary>
        /// Street of the address.
        /// </summary>
        public System.String Street { get; set; }


        /// <summary>
        /// House number of the address, without any extension.
        /// </summary>
        public System.String HouseNumber { get; set; }


        /// <summary>
        /// Addition or extension of the house number.
        /// </summary>
        public System.String HouseNumberAddition { get; set; }


        /// <summary>
        /// Postal code of the address
        /// </summary>
        public System.String PostalCode { get; set; }


        /// <summary>
        /// The city of the address.
        /// </summary>
        public System.String City { get; set; }


        /// <summary>
        /// ISO 3166-1 alpha-2 country code.
        /// </summary>
        public System.String Country { get; set; }
    }
}
