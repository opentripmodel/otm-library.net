using System.Text.RegularExpressions;

namespace OpenTripModel.Profile.Internal;

/// <summary>Holds every compiled regex used in the codebase.</summary>
internal static class RegexRegistry
{
    /// <summary>
    /// Captures the property name(s) inside the ["prop"] list of a JSON-Schema
    /// “required” validation error.
    ///   Sample match target:  Required properties ["title"] are not present
    ///   Group 1 captures:     title
    /// </summary>
    public static readonly Regex MissingRequiredProperty = new Regex(@"\[\""(.*?)\""\]", RegexOptions.Compiled);


    /// <summary>
    /// Captures the last token of a JSON Pointer.
    ///   Example pointer:  #/foo/bar  →  Match.Value = "bar"
    /// </summary>
    public static readonly Regex JsonPointerLastToken = new Regex(@"[^/]+$", RegexOptions.Compiled);
}
