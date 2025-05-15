using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class GoodItem
        : OtmEntity
    {
        public GoodItemType Type { get; set; }


        /// <summary>
        /// A free text description of these goods.
        /// </summary>
        public System.String Description { get; set; }


        /// <summary>
        /// Remark belonging to the goods that need to be transported. For example 
        /// a delivery note.
        /// </summary>
        public System.String Remark { get; set; }


        /// <summary>
        /// A barcode present on the (packaging of the) goods that uniquely identifies 
        /// these goods.
        /// </summary>
        public System.String BarCode { get; set; }


        /// <summary>
        /// A quantity determines how many of a certain good you have. Note that all 
        /// other measurements are measured for a single product, not for the total of 
        /// products.
        /// </summary>
        public System.Double Quantity { get; set; }

        
        /// <summary>
        /// The weight of a 'single' good, the total weight can be calculated by using 
        /// the quantity and multiplying it with the weight.
        /// </summary>
        public UnitWithValue Weight { get; set; }


        /// <summary>
        /// The width of the Vehicle
        /// </summary>
        public UnitWithValue Width { get; set; }


        /// <summary>
        /// The height of the Vehicle
        /// </summary>
        public UnitWithValue Height { get; set; }


        /// <summary>
        /// The length of the Vehicle
        /// </summary>
        public UnitWithValue Length { get; set; }


        /// <summary>
        /// Information about the potentially dangerous properties of these goods.
        /// </summary>
        public Adr Adr { get; set; }


        /// <summary>
        /// The product type of goods, for instance bananas.
        /// </summary>
        public System.String ProductType { get; set; }


        /// <summary>
        /// Description of the package type f.e. pallet, europallet, drum, carton etc.
        /// </summary>
        public System.String PackagingMaterial { get; set; }


        /// <summary>
        /// All parties associated with these goods, for example the consignor and consignee.
        /// </summary>
        public List<InlineAssociationActorType> Actors { get; set; }



        /// <summary>
        /// Associations actions
        /// </summary>
        public List<InlineAssociationType<Action>> Actions { get; set; }


        /// <summary>
        /// The constraints this action abides to, such as start and end time windows.
        /// </summary>
        public InlineAssociationType<Constraint> Constraint { get; set; }
    }
}