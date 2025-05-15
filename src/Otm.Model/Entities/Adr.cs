using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class Adr
    {
        public System.String UnNumber { get; set; }

        public System.String Language { get; set; }

        public System.String Description { get; set; }

        public System.String TechnicalName { get; set; }

        public System.Boolean Waste { get; set; }

        public System.Boolean EnvironmentallyHazardous { get; set; }

        public System.String Class { get; set; }

        public System.String ClassificationCode { get; set; }

        public System.String PackagingGroup { get; set; }

        public IEnumerable<System.String> DangerLabels { get; set; }

        public System.String DangerNumber { get; set; }

        public System.String TunnelCode { get; set; }
    }
}
