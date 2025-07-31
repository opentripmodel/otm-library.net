namespace OpenTripModel.Profile.Internal;

/// <summary>
/// Central location for literal text fragments
/// </summary>
internal static class TextResources
{
    internal static class JsonSchemaTokens
    {
        public const string RequiredSuffix       = "are not present";
        public const string ValueIsPrefix        = "Value is";
        public const string ButShouldBeConnector = "but should be";
    }

    /// <summary>
    /// User-friendly text that replaces raw validator fragments.
    /// All strings in this block are ultimately shown to users
    /// </summary>
    internal static class UserFriendlyOverrides
    {
        public const string RequiredSuffix       = "is missing";
        public const string ExpectedConnector    = "; expected";
        public const string AdditionalTemplate   = "Property \"{0}\" is not allowed by the schema.";
        public const string EnumMessage          = "Value is not one of the permitted options.";
        public const string ActualTypePrefix     = "Actual type is";
    }

}
