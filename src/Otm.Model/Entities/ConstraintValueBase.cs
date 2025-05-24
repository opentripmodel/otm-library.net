namespace OpenTripModel.v5
{
    public abstract class ConstraintValueBase
    {
        public abstract ConstraintTypeEnum Type { get; }

        public System.String Description { get; set; }
    }
}
