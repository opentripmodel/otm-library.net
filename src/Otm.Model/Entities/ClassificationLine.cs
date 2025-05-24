using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OpenTripModel.v5
{
    /// <summary>
    /// Product classification information often required at customs. A single product can contain multiple 
    /// classification lines. For example whenever the product consists of multiple components that can each 
    /// be classified. The most important information in the classification lines is often the HS code.
    /// </summary>
    public class ClassificationLine
    {
        /// <summary>
        /// The Harmonized System (HS) code is a product classification code used by all members of the
        /// World Customs Organization (WCO) to classify goods for customs purposes.
        /// </summary>
        public System.String HsCode { get; set; }


        /// <summary>
        /// The description of this classification line.
        /// </summary>
        public System.String Description { get; set; }


        /// <summary>
        /// A quantity determines how many of a certain good you have. Note that all other measurements
        /// are measured for a single product, not for the total of products.
        /// </summary>
        public Nullable<System.Int32> Quantity { get; set; }


        /// <summary>
        /// The net weight of a 'single' good, the total weight can be calculated by using the quantity 
        /// and multiplying it with this weight.
        /// </summary>
        public UnitWithValue Weight { get; set; }


        /// <summary>
        /// The gross weight of a 'single' good, the total weight can be calculated by using the 
        /// quantity and multiplying it with this weight.
        /// </summary>
        public UnitWithValue GrossWeight { get; set; }


        /// <summary>
        /// The width of a 'single' classified goods part, the total width can be calculated by 
        /// using the quantity and multiplying it with this width
        /// </summary>
        public UnitWithValue Width { get; set; }
        
        
        /// <summary>
        /// The Harmonized System (HS) code is a product classification code used by all members of the
        /// World Customs Organization (WCO) to classify goods for customs purposes.
        /// </summary>
        public UnitWithValue Height { get; set; }


        /// <summary>
        /// The length of a 'single' classified goods part, the total length can be calculated by using 
        /// the quantity and multiplying it with this length.
        /// </summary>
        public UnitWithValue Length { get; set; }


        /// <summary>
        /// Fallback solution as a stopgap for fields not yet in otm.You can use these, but please file a 
        /// change request so that they can be properly incorporated into the specification.
        /// </summary>
        public List<ClassificationField> Others { get; set; }
    }
}
