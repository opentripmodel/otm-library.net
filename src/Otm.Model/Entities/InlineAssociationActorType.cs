using System.Collections.Generic;

namespace OpenTripModel.v5
{
    public class InlineAssociationActorType
        : InlineAssociationType<Actor>
    {
        public IEnumerable<System.String> Roles { get; set; }
    }
}
