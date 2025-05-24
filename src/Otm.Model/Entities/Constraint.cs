namespace OpenTripModel.v5
{
    public class Constraint
        : OtmEntity
    {
        public ConstraintValueBase Value { get; set; }

        public Enforceability Enforceability { get; set; } = Enforceability.Enforced;
    }
}